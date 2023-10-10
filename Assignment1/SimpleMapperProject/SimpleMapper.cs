using System;
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
            var sourceProperties = sourceType.GetProperties(BindingFlags.Public|BindingFlags.Instance|BindingFlags.DeclaredOnly);
            var destinationProperties = destinationType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            if (Equals(sourceType,destinationType)&& Equals(sourceProperties, sourceProperties))
            {
                foreach( var property in sourceProperties )
                {
                    Console.WriteLine(property.Name);
                }
                foreach( var property in destinationProperties)
                {
                    Console.WriteLine(property.Name);
                }
            }
            else
            {
                Console.WriteLine("The destination object or property  is different from the Source Object");
            }
        }
    }
}
