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
        private string connectionString = "Server=DESKTOP-LNM2C8V\\SQLEXPRESS;Database=AspnetB9;User Id=aspnetb9;Password=123456;TrustServerCertificate=True;";

        public void Insert(T entity)
        {
            Type entityType = typeof(T);
            string tableName = entityType.Name;

            PropertyInfo[] properties = entityType.GetProperties();

            PropertyInfo parentIdProp = entityType.GetProperty("Id");
            var parentId = parentIdProp.GetValue(entity, null);


            foreach(PropertyInfo property in properties)
            {
                if (IsNestedObject(property))
                {
                    InsertNestedObject(property, entity,tableName, parentId);
                }
                else if(property.Name == "Id") 
                { 
                    Console.WriteLine(property.Name + "Primptive");
                    Console.WriteLine(parentId);
                }
            }

            // Create INSERT statement for the main entity table
            string insertMainEntity = GenerateInsertStatement(tableName, properties);

            // Execute the INSERT statement for the main entity
            var id =  ExecuteNonQuery(insertMainEntity, properties, entity);
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

                PropertyInfo nestedIdProp = nestedType.GetProperty("Id");
                var nestedId = nestedIdProp.GetValue(nestedObject);

                foreach (var nestedProp in nestedProperties)
                {
                    if (IsNestedObject(nestedProp))
                    {
                        InsertNestedObject(nestedProp, nestedObject, nestedTableName, nestedId);
                    }
                    else if (nestedProp.Name == "Id")
                    {
                        Console.WriteLine(nestedProp.Name + "Primitive");
                        Console.WriteLine(nestedId);
                    }
                }


                var insertelements = new List<PropertyInfo>(); 
                foreach (var nestedProp in nestedProperties)
                {
                    if (!IsNestedObject(nestedProp))
                    {
                        insertelements.Add(nestedProp);
                    }
                }



               // Create INSERT statement for the nested entity table
               string insertNestedEntity = GenerateNestedInsertStatement(nestedTableName, insertelements.ToArray(), parentTableName, parentId);

                // Execute the INSERT statement for the nested entity
                var nestedIdResult = ExecuteNonQuery(insertNestedEntity, insertelements.ToArray(), nestedObject);

                // Use the nestedIdResult if needed
            }
        }

        private void ExecuteNestedNonQuery(string query, PropertyInfo[] properties, object entity)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    foreach (var property in properties)
                    {
                        if (!IsNestedObject(property))
                        {
                            // Explicitly map .NET types to SQL data types
                            SqlParameter parameter = new SqlParameter($"@{property.Name}", GetSqlDbType(property.PropertyType))
                            {
                                Value = property.GetValue(entity) ?? DBNull.Value
                            };

                            command.Parameters.Add(parameter);
                        }
                    }
   
                    //need to insert the parent table id through parameter
                    command.ExecuteNonQuery();
                }
            }
        }

        private string GenerateNestedInsertStatement(string tableName, PropertyInfo[] properties, string parentTableName, object? parentId)
        {
            string columns = string.Join(", ", properties.Select(p =>  p.Name));
            string values = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            return $"INSERT INTO {tableName} ({columns},{parentTableName + "Id"}) VALUES ({values},{parentId});";
        }

        private int ExecuteNonQuery(string query, PropertyInfo[] properties, object entity)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                        connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // First, handle nested objects
                    foreach (var property in properties)
                    {
                        if (IsNestedObject(property))
                        {
                            PropertyInfo[] nestedProperties = property.PropertyType.GetProperties();

                            // Get the nested object value
                            var nestedObject = property.GetValue(entity);

                            // If the nested object is not null, insert its data first
                            if (nestedObject != null)
                            {
                                // Recursively call ExecuteNonQuery for the nested object
                                id = ExecuteNonQuery(GenerateInsertStatement(property.PropertyType.Name, nestedProperties), nestedProperties, nestedObject);

                                // Now, get the ID of the inserted nested object
                                int nestedObjectId = GetInsertedId(connection, property.PropertyType.Name);


                                // Set the nested object ID in the parent entity
                                SetPropertyValue(entity, property.Name + "Id", nestedObjectId);
                            }
                        }
                    }

                    // Now, handle the main entity
                    foreach (var property in properties)
                    {
                        if (!IsNestedObject(property))
                        {
                            // Explicitly map .NET types to SQL data types
                            SqlParameter parameter = new SqlParameter($"@{property.Name}", GetSqlDbType(property.PropertyType))
                            {
                                Value = property.GetValue(entity) ?? DBNull.Value
                            };

                            command.Parameters.Add(parameter);
                        }
                    }

                    command.ExecuteNonQuery();
                }
            }
            return id;
        }

        private string GenerateInsertStatement(string tableName, PropertyInfo[] properties)
        {
            string columns = string.Join(", ", properties.Select(p => IsNestedObject(p)?p.Name+"Id":p.Name));
            string values = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            return $"INSERT INTO {tableName} ({columns}) VALUES ({values}); SELECT @@IDENTITY;";
        }

        private SqlDbType GetSqlDbType(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.String:
                    return SqlDbType.NVarChar;
                case TypeCode.Int32:
                    return SqlDbType.Int;
                // Add more cases as needed
                default:
                    throw new ArgumentException($"Unsupported data type: {type.Name}");
            }
        }

        private int GetInsertedId(SqlConnection connection, string tableName)
        {
            string query = $"SELECT IDENT_CURRENT('{tableName}')";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                object result = command.ExecuteScalar();

                if (result != DBNull.Value && result != null)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    // Handle the case where the result is DBNull.Value or null
                    // You can return a default value or throw an exception based on your needs
                    throw new InvalidOperationException("Unable to retrieve the inserted ID.");
                }
            }
        }

        // Usage in your ExecuteNonQuery method


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
