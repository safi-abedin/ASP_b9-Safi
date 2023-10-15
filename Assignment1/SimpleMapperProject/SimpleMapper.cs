using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapperProject
{
    public static class SimpleMapper
    {
        public static void Copy(object source, object destination)
        {
            // Checking source or destination
            if (source == null )
            {
                throw new ArgumentNullException("Source cannot be null");
            }

            var sourceType = source.GetType();
            var destinationType = destination.GetType();


            var sourceProperties = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (var property in sourceProperties)
            {
                var destProperty = destinationType.GetProperty(property.Name);
                var srcValue = property.GetValue(source);

                if (!property.CanWrite || property == null)
                {

                    continue;
                }

                if (destProperty == null)
                    continue;


                if (property.PropertyType.IsPrimitive || property.PropertyType == typeof(string))
                {
                    destProperty.SetValue(destination, srcValue);
                }
                else if (property.PropertyType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
                {
                    // Handle IEnumerable properties (e.g., lists)
                    var sourceList = srcValue as IEnumerable;
                    Type srcType = sourceList.GetType();
                    if (sourceList != null)
                    { 
                        if(srcType.IsArray)
                        {
                            Console.WriteLine("Inside");
                            Console.WriteLine(property.Name);
                            /*if (property.GetValue(srcType, null) != null)
                            {
                                Array values = property.GetValue(sourceList, null) as Array;
                                var qtyItems = values.Length;
                                foreach (var value in values)
                                {
                                    Console.WriteLine(value);
                                }
                            }*/
                            // Handle array properties
                            var sourceArray = (Array)sourceValue;
                            var destinationArray = Array.CreateInstance(destinationProperty.PropertyType.GetElementType(), sourceArray.Length);

                            for (int i = 0; i < sourceArray.Length; i++)
                            {
                                var sourceArrayElement = sourceArray.GetValue(i);
                                var destinationArrayElement = Activator.CreateInstance(destinationProperty.PropertyType.GetElementType());

                                CopyProperties(sourceArrayElement, destinationArrayElement);
                                destinationArray.SetValue(destinationArrayElement, i);
                            }

                            destinationProperty.SetValue(destination, destinationArray);
                            var ins = Array.CreateInstance(property.PropertyType.GetElementType(), 4);
                            Console.WriteLine(ins);
                            /*ConstructorInfo constructorInfo = srcType.GetConstructor(new Type[] { });
                            object instance = constructorInfo.Invoke(srcType, null);
                            Console.WriteLine(instance.ToString);*/
                        }
                        else   
                        {
                            var destList = (IList)Activator.CreateInstance(destProperty.PropertyType);
                            foreach (var item in sourceList)
                            {
                                if (item.GetType().IsPrimitive || item is string)
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
                    }
                }
                else
                {
                    // For other complex types, recursively copy
                    var newDest = Activator.CreateInstance(destProperty.PropertyType);
                    Copy(srcValue, newDest);
                    destProperty.SetValue(destination, newDest);
                }
            }
        }
    }
}
