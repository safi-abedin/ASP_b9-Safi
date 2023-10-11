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
            if(sourceType != destinationType )
            {  
                throw new Exception("Source and Destination Type are not same");
            }

            //geeting the properties that only decleared to match the properties and checks if both contains same property 
            var sourceProperties = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach(var property in sourceProperties)
            {
                if (!property.CanWrite || property == null) { continue; }
                
                if (sourceType.GetProperty(property.Name) != destinationType.GetProperty(property.Name))
                {
                    continue;
                }
                else
                {
                    if (property.PropertyType.IsPrimitive)
                    {
                        Setvalue(source,destination,property);
                        
                    }
                    else if (property.PropertyType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
                    {
                        var items = (IEnumerable)property.GetValue(source);
                        if (items != null && items.GetType() != typeof(string))
                        {
                            var destProperty = destinationType.GetProperty(property.Name);
                            var destCollection = (IList) Activator.CreateInstance(destProperty.PropertyType);
                            var democollection = (IList)Activator.CreateInstance(destProperty.PropertyType);
                            foreach (var item in items)
                            {
                                if (!(item is string))
                                {
                                    var newItem = Activator.CreateInstance(destProperty.PropertyType.GenericTypeArguments[0]);
                                    Copy(item, newItem);
                                    destCollection.Add(newItem);
                                }
                                else 
                                {
                                    var data = democollection.Add(string.Join(string.Empty, item));
                                    Console.WriteLine(data.GetType().GetInterfaces().Contains(typeof(string)));
                                }
                            }
                            destProperty.SetValue(destination, destCollection);
                        }
                        else
                        {
                            Setvalue(source, destination, property);
                        }
                    }
                    else
                    {
                        Setvalue(source, destination, property);
                    }
                }
            }
           
        }

        private static void Setvalue(object source, object destination, PropertyInfo property)
        {
            var srcvalue = property.GetValue(source);
            property.SetValue(destination, srcvalue);
        }
    }
}
