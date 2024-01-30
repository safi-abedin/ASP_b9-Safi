using System;
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

            // Create INSERT statement for the main entity table
            string insertMainEntity = GenerateInsertStatement(tableName, properties);

            // Execute the INSERT statement for the main entity
            ExecuteNonQuery(insertMainEntity, properties, entity);
        }

        private void ExecuteNonQuery(string query, PropertyInfo[] properties, object entity)
        {
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
                                ExecuteNonQuery(GenerateInsertStatement(property.PropertyType.Name, nestedProperties), nestedProperties, nestedObject);

                                // Now, get the ID of the inserted nested object
                                int nestedObjectId = GetInsertedId(connection);

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
        }

        private string GenerateInsertStatement(string tableName, PropertyInfo[] properties)
        {
            string columns = string.Join(", ", properties.Select(p => p.Name));
            string values = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            return $"INSERT INTO {tableName} ({columns}) VALUES ({values}); SELECT SCOPE_IDENTITY();";
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

        private int GetInsertedId(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT SCOPE_IDENTITY()", connection))
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
