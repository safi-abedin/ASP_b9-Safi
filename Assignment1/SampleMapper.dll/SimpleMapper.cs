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
        public static void Copy(object source, object destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException("Source cannot be null");
            }

            if (destination == null)
            {
                throw new ArgumentNullException("Destination cannot be null");
            }

            CopyProperties(source, destination);
        }

        private static void CopyProperties(object source, object destination)
        {
            var sourceProperties = source.GetType().GetProperties();
            var destinationProperties = destination.GetType().GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                var destinationProperty = destinationProperties.FirstOrDefault(
                    prop => prop.Name == sourceProperty.Name && prop.PropertyType == sourceProperty.PropertyType);

                if (destinationProperty != null && destinationProperty.CanWrite)
                {
                    var sourceValue = sourceProperty.GetValue(source);

                    if (sourceProperty.PropertyType.IsArray)
                    {
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
                    }
                    else if (sourceProperty.PropertyType.IsClass)
                    {
                        // Handle complex type properties (nested objects)
                        var destinationValue = Activator.CreateInstance(destinationProperty.PropertyType);
                        CopyProperties(sourceValue, destinationValue);
                        destinationProperty.SetValue(destination, destinationValue);
                    }
                    else
                    {
                        // Handle primitive and simple properties
                        destinationProperty.SetValue(destination, sourceValue);
                    }
                }
            }
        }
    }
}