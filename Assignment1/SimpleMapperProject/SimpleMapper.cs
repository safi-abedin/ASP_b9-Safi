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
        public static void Copy(object source, object destination)
        {
            //checking  source or destination 
            if(source == null || destination == null)
            {
                throw new ArgumentNullException("Source or destination can not be null");
            }


            //getting the type of objects And checks for the if both type are same
            var sourceType = source.GetType();
            var destinationType = destination.GetType();
            if(sourceType != destinationType)
            {
                throw new Exception("Source and Destination Type are not same");
            }

            //geeting the properties that only decleared to match the properties and checks if both contains same property 
            var sourceProperties = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach(var property in sourceProperties)
            {
                if (sourceType.GetProperty(property.Name) != destinationType.GetProperty(property.Name))
                {
                    continue;
                }
                else
                {
                    if (property.PropertyType.IsPrimitive)
                    {
                        var srcvalue = property.GetValue(source);
                        property.SetValue(destination, srcvalue);
                    }
                    else if (property.PropertyType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
                    {
                        var items = (IEnumerable)property.GetValue(source);
                        if (items != null && items.GetType() != typeof(string))
                        {
                            /*foreach (var item in items)
                            {
                                var itemProperties = item.GetType().GetProperties();
                                Copy(itemProperties, item);
                            }*/
                        }
                        else
                        {
                            var Strvalue  = property.GetValue(source);
                            property.SetValue(destination, Strvalue);
                        }
                    }
                    else
                    {
                        var propertyValue = property.GetValue(source);
                        property.SetValue(destination, propertyValue);
                    }
                }
            }
           
        }
    }
}
