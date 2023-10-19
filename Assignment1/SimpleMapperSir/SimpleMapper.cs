using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapperSir
{
    public static class SimpleMapper
    {
        public static void Copy(object source, object destination)
        {
            // Checking source 
            if (source == null)
            {
                throw new ArgumentNullException("Source cannot be null");
            }

            // getting the types of source and destination and the source Properties
            var sourceType = source.GetType();
            var destinationType = destination.GetType();
            var sourceProperties = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            //Enumarating  all properties  to copy
            foreach (var property in sourceProperties)
            {
                //getting destination property and and source value and cheking if we can copy
                var destProperty = destinationType.GetProperty(property.Name);
                var srcValue = property.GetValue(source);
                if (!property.CanWrite || property == null || destProperty == null)
                {
                    continue;
                }


                // if we get prmitive type property or string then we will simply copy them  
                if (property.PropertyType.IsPrimitive || property.PropertyType == typeof(string))
                {
                    destProperty.SetValue(destination, srcValue);
                }
                // if we get any type that is IEnumerable like-List,Array,Dictionary etc then this condition will execute 
                else if (property.PropertyType.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
                {
                    CopyIEnumerable(srcValue, destination, property, destProperty);
                }
                else
                {
                    // For other complex types like - user Created class, recursively copy
                    var newDest = Activator.CreateInstance(destProperty.PropertyType);
                    Copy(srcValue, newDest);
                    destProperty.SetValue(destination, newDest);
                }
            }
        }

        private static void CopyIEnumerable(object? srcValue, object destination, PropertyInfo property, PropertyInfo destProperty)
        {
            // loading the value as IEnumerable and further we will check if we have any nested object copy 
            var sourceList = srcValue as IEnumerable;

            if (sourceList != null)
            {
                Type srcType = sourceList.GetType();
                if (srcType.IsArray)
                {
                    // Handle array properties
                    HandleArrayType(sourceList, property, destination, destProperty);
                }
                else
                {
                    //handle the list property 
                    HandleListType(destProperty, sourceList, destination);
                }
            }
        }

        //Handle all type of list including inner array
        private static void HandleListType(PropertyInfo destProperty, IEnumerable sourceList, object destination)
        {
            Type elementType = destProperty.PropertyType.GenericTypeArguments[0];
            IList destList = (IList)Activator.CreateInstance(destProperty.PropertyType);

            foreach (var item in sourceList)
            {
                //for inner array in list if needed and the array should be in destination to copy
                if (item is IEnumerable innerArray && elementType.GetElementType() is not null)
                {
                    var arrayElementType = elementType.GetElementType();
                    Console.WriteLine(arrayElementType.Name);
                    var array = Array.CreateInstance(arrayElementType, innerArray.Cast<object>().Count());

                    int index = 0;
                    foreach (var innerItem in innerArray)
                    {
                        var innerDest = Activator.CreateInstance(arrayElementType);
                        Copy(innerItem, innerDest);
                        array.SetValue(innerDest, index);
                        index++;
                    }

                    destList.Add(array);
                }
                else if (item.GetType().IsPrimitive || item is string)
                {
                    // For primitive types or strings, simply add to the destination list
                    destList.Add(item);
                }
                else
                {
                    // For complex types, recursively copy
                    var newItem = Activator.CreateInstance(destProperty.PropertyType.GenericTypeArguments[0]);
                    Copy(item, newItem);
                    destList.Add(newItem);
                }
            }

            destProperty.SetValue(destination, destList);
        }


        //Handled Cases for all kind of array
        private static void HandleArrayType(IEnumerable sourceList, PropertyInfo property, object destination, PropertyInfo destProperty)
        {
            var srcListToArray = sourceList as Array;
            var instance = Array.CreateInstance(property.PropertyType.GetElementType(), srcListToArray.Length);
            Array.Copy(srcListToArray, instance, srcListToArray.Length);
            destProperty.SetValue(destination, instance);
        }
    }
}
