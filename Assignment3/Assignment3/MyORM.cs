using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Assignment3
{
    public class MyORM<G, T> where T : class, IEntity<G>
    {
        private string connectionString = ".\\SQLEXPRESS;Database=AspnetB9;User Id=aspnetb9;Password=123456;TrustServerCertificate=True;";

        public void Insert(T entity)
        {
            if(entity == null) { throw new ArgumentNullException("Source Can not be null"); }

            Type entityType = typeof(T);
            string tableName = entityType.Name;
            PropertyInfo[] properties = entityType.GetProperties();


            //First insert the primtive properties
            var insertelements = new List<PropertyInfo>();
            foreach (var property in properties)
            {
                if (!IsNestedObject(property))
                {
                    insertelements.Add(property);
                }
            }

            string insertMainEntity = GenerateInsertStatement(tableName, insertelements.ToArray());
            ExecuteNonQuery(insertMainEntity, properties, entity);


            //then the nested object
            PropertyInfo parentIdProp = entityType.GetProperty("Id");
            var parentId = parentIdProp.GetValue(entity, null);

            foreach(PropertyInfo property in properties)
            {
                if (IsNestedObject(property))
                {
                    InsertNestedObject(property, entity,tableName, parentId);
                }
            }
        }


        private void InsertNestedObject(PropertyInfo nestedProperty, object entity, string parentTableName, object? parentId)
        {
            var type = nestedProperty.PropertyType;
            var nestedObject = nestedProperty.GetValue(entity);

            if (nestedObject != null)
            {
                var nestedType = nestedObject.GetType();
                var nestedTableName = nestedType.Name;

                Console.WriteLine(nestedTableName);

                PropertyInfo[] nestedProperties = nestedType.GetProperties();

                //first insert the nested primptive types

                var insertelements = new List<PropertyInfo>();
                foreach (var nestedProp in nestedProperties)
                {
                    if (!IsNestedObject(nestedProp))
                    {
                        insertelements.Add(nestedProp);
                    }
                }
                string insertMainEntity = GenerateInsertStatement(nestedTableName, insertelements.ToArray());
                ExecuteNonQuery(insertMainEntity, insertelements.ToArray(), entity);

                //then check if any nested object to travarse

                PropertyInfo nestedIdProp = nestedType.GetProperty("Id");
                var nestedId = nestedIdProp.GetValue(nestedObject);

                /*
                var insertelementsNested = new List<PropertyInfo>();
                foreach (var nestedProp in nestedProperties)
                {
                    if (!IsNestedObject(nestedProp))
                    {
                        insertelementsNested.Add(nestedProp);
                    }
                }

                // Create INSERT statement for the nested entity table
                string insertNestedEntity = GenerateNestedInsertStatement(nestedTableName, insertelements.ToArray(), parentTableName, parentId);

                // Execute the INSERT statement for the nested entity
                ExecuteNonQuery(insertNestedEntity, insertelements.ToArray(), nestedObject);*/

                foreach (var nestedProp in nestedProperties)
                {
                    if (IsNestedObject(nestedProp))
                    {
                        InsertNestedObject(nestedProp, nestedObject, nestedTableName, nestedId);
                    }
                }
            }
        }

        //generating query for the object it self without including nested object
        private string GenerateInsertStatement(string tableName, PropertyInfo[] properties)
        {
            string columns = string.Join(", ", properties.Select(p => p.Name));
            string values = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            return $"INSERT INTO {tableName} ({columns}) VALUES ({values});";
        }

        //generating query for nested object with parent table id reference
        private string GenerateNestedInsertStatement(string tableName, PropertyInfo[] properties, string parentTableName, object? parentId)
        {
            string columns = string.Join(", ", properties.Select(p =>  p.Name));
            string values = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            return $"INSERT INTO {tableName} ({columns},{parentTableName + "Id"}) VALUES ({values},{parentId});";
        }


        //to execute  query 
        private void ExecuteNonQuery(string query, PropertyInfo[] properties, object entity)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    foreach (var property in properties)
                    {
                            SqlParameter parameter = new SqlParameter($"@{property.Name}", GetSqlDbType(property.PropertyType))
                            {
                                Value = property.GetValue(entity) ?? DBNull.Value
                            };

                            command.Parameters.Add(parameter);
                    }
                    command.ExecuteNonQuery();
                }
            }
        }

        private SqlDbType GetSqlDbType(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.String:
                    return SqlDbType.NVarChar;
                case TypeCode.Int32:
                    return SqlDbType.Int;
                case TypeCode.Double:
                    return SqlDbType.Float;
                default:
                    throw new ArgumentException($"Unsupported data type: {type.Name}");
            }
        }


        private void SetPropertyValue(object obj, string propertyName, object value)
        {
            var property = obj.GetType().GetProperty(propertyName);
            property?.SetValue(obj, value);
        }

        private bool IsNestedObject(PropertyInfo property)
        {
            return property.PropertyType.IsClass && property.PropertyType != typeof(string);
        }
    }
}
