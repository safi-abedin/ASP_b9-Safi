using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapper.dll
{
    public class SimpleMapper
    {
        public  void Copy(object source, object destination)
        {
            // Checking source or destination
            if (source == null)
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

                if (srcValue is null)
                {
                    destProperty.SetValue(destination, "No value");
                }

                if (property.PropertyType.IsPrimitive || property.PropertyType == typeof(string))
                {
                    destProperty.SetValue(destination, srcValue);
                }
                else if (property.PropertyType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
                {
                    // Handle IEnumerable properties (e.g., lists)
                    var sourceList = srcValue as IEnumerable;
                    if (sourceList != null)
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
