using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapperProject
{
    public class SimpleMapper
    {
        public void Copy(object source, object destination)
        {
            //getting the type of objects
            var sourceType = source.GetType();
            var destinationType = destination.GetType();
            //geeting the properties that only decleared to match the properties
            var sourceProperties = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            var destinationProperties = destinationType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            if (Equals(sourceType, destinationType) && Equals(sourceProperties, sourceProperties))
            {
                foreach (var property in sourceProperties)
                {
                    Console.WriteLine(property.Name);
                }
                foreach (var property in destinationProperties)
                {
                    Console.WriteLine(property.Name);
                }
            }
            else
            {
                Console.WriteLine("The destination object or property  is different from the Source Object");
            }
        }

       /* public void Recursion(PropertyInfo[] srcPropertyInfo, object srcObj, PropertyInfo[] dstPropertyInfo, object dstObj)
        {
            foreach (var property in srcPropertyInfo)
            {
                if (property.PropertyType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
                {
                    var items = (IEnumerable)property.GetValue(srcObj);
                    if (items != null && items.GetType() != typeof(string))
                    {
                        foreach (var item in items)
                        {
                            var itemProperties = item.GetType().GetProperties();
                            Recursion(itemProperties, item);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{property.Name} : {items}");
                    }
                }
                else
                {
                    var propertyValue = property.GetValue(obj);
                    Console.WriteLine($"{property.Name}: {propertyValue}");
                }
            }
        }*/
    }
}
