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
        private string connectionString = "Data Source=.\\SQLEXPRESS;Database=AspnetB9;User Id=aspnetb9;Password=123456;TrustServerCertificate=True;";

        public void Insert(T entity)
        {
            if (entity == null) { throw new ArgumentNullException("Source Can not be null"); }

            Type entityType = typeof(T);
            string tableName = entityType.Name;
            PropertyInfo[] properties = entityType.GetProperties();

            // First insert the primitive properties
            var insertElements = new List<PropertyInfo>();
            foreach (var property in properties)
            {
                if (!IsNestedObject(property))
                {
                    insertElements.Add(property);
                }
            }

            string insertMainEntity = GenerateInsertStatement(tableName, insertElements.ToArray());
            ExecuteNonQuery(insertMainEntity, properties, entity);

            // Then the nested object
            PropertyInfo parentIdProp = entityType.GetProperty("Id");
            var parentId = parentIdProp.GetValue(entity, null);

            foreach (PropertyInfo property in properties)
            {
                if (IsNestedObject(property))
                {
                    InsertNestedObject(property, entity, tableName, parentId);
                }
            }
        }

        public void Update(T entity)
        {
            if (entity == null) { throw new ArgumentNullException("Source Can not be null"); }

            Type entityType = typeof(T);
            string tableName = entityType.Name;
            PropertyInfo[] properties = entityType.GetProperties();

            // Update the main entity
            var updateElements = new List<PropertyInfo>();
            foreach (var property in properties)
            {
                if (!IsNestedObject(property) && property.Name != "Id")
                {
                    updateElements.Add(property);
                }
            }

            PropertyInfo idProp = entityType.GetProperty("Id");
            var idValue = idProp.GetValue(entity);

            string updateMainEntity = GenerateUpdateStatement(tableName, updateElements.ToArray(), idProp.Name);
            ExecuteNonQuery(updateMainEntity, properties, entity);

            // Update nested objects
            foreach (PropertyInfo property in properties)
            {
                if (IsNestedObject(property))
                {
                    UpdateNestedObject(property, entity, tableName, idValue);
                }
            }
        }

        public void Delete(T entity)
        {
            Type entityType = typeof(T);
            string tableName = entityType.Name;
            PropertyInfo[] properties = entityType.GetProperties();

            //first We have to Delete the nested Objects

            PropertyInfo parentIdProp = entityType.GetProperty("Id");
            var parentId = parentIdProp.GetValue(entity, null);

            foreach (PropertyInfo property in properties)
            {
                if (IsNestedObject(property))
                {
                    DeleteNestedObject(property, entity, tableName, parentId);
                }
            }

            //then we will delete the main entity

             string query = GenerateDeleteStatement(tableName, parentId);
             ExecuteDeleteQuery(query);
            
        }

        private void DeleteNestedObject(PropertyInfo nestedProperty, object entity, string parentTableName, object? parentId)
        {
            var type = nestedProperty.PropertyType;
            var nestedObject = nestedProperty.GetValue(entity);

            if (nestedObject != null)
            {
                if (IsEnumerableType(type))
                {
                    // Handle IEnumerable or List<T> type
                    IEnumerable<object> nestedList = nestedObject as IEnumerable<object>;
                    if (nestedList != null)
                    {
                        foreach (var item in nestedList)
                        {
                            DeleteNestedObjectItem(nestedProperty, item, parentTableName, parentId);
                        }
                    }
                }
                else
                {
                    // Handle single nested object
                    DeleteNestedObjectItem(nestedProperty, nestedObject, parentTableName, parentId);
                }
            }
        }

        private void DeleteNestedObjectItem(PropertyInfo nestedProperty, object nestedObject, string parentTableName, object? parentId)
        {
            var nestedType = nestedObject.GetType();
            var nestedTableName = nestedType.Name;
            PropertyInfo[] nestedProperties = nestedType.GetProperties();

            PropertyInfo nestedIdProp = nestedType.GetProperty("Id");
            var nestedId = nestedIdProp.GetValue(nestedObject);

            //if any nested object delete first 
            foreach (var nestedProp in nestedProperties)
            {
                if (IsNestedObject(nestedProp))
                {
                    DeleteNestedObject(nestedProp, nestedObject, nestedTableName, nestedId);
                }
            }

            var deleteQuery = GenerateNestedDeleteStatement(nestedTableName, parentTableName, parentId);
            ExecuteDeleteQuery(deleteQuery);
        }

        private void ExecuteDeleteQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private string GenerateNestedDeleteStatement(string nestedTableName, string parentTableName, object? parentId)
        {
            return $"DELETE FROM {nestedTableName} WHERE {parentTableName}Id={parentId};";
        }

        private string GenerateDeleteStatement(string tableName, object Id)
        {
            return $"DELETE FROM {tableName} WHERE Id={Id};";
        }

        private void InsertNestedObject(PropertyInfo nestedProperty, object entity, string parentTableName, object? parentId)
        {
            var type = nestedProperty.PropertyType;
            var nestedObject = nestedProperty.GetValue(entity);

            if (nestedObject != null)
            {
                if (IsEnumerableType(type))
                {
                    // Handle IEnumerable or List<T> type
                    IEnumerable<object> nestedList = nestedObject as IEnumerable<object>;
                    if (nestedList != null)
                    {
                        foreach (var item in nestedList)
                        {
                            InsertNestedObjectItem(nestedProperty, item, parentTableName, parentId);
                        }
                    }
                }
                else
                {
                    // Handle single nested object
                    InsertNestedObjectItem(nestedProperty, nestedObject, parentTableName, parentId);
                }
            }
        }

        private void InsertNestedObjectItem(PropertyInfo nestedProperty, object nestedObject, string parentTableName, object? parentId)
        {
            var nestedType = nestedObject.GetType();
            var nestedTableName = nestedType.Name;
            PropertyInfo[] nestedProperties = nestedType.GetProperties();

            // First insert the nested primitive types with parent id
            PropertyInfo nestedIdProp = nestedType.GetProperty("Id");
            var nestedId = nestedIdProp.GetValue(nestedObject);

            var insertElementsNested = new List<PropertyInfo>();
            foreach (var nestedProp in nestedProperties)
            {
                if (!IsNestedObject(nestedProp))
                {
                    insertElementsNested.Add(nestedProp);
                }
            }

            // Create INSERT statement for the nested entity table
            string insertNestedEntity = GenerateNestedInsertStatement(nestedTableName, insertElementsNested.ToArray(), parentTableName, parentId);

            // Execute the INSERT statement for the nested entity
            ExecuteNonQuery(insertNestedEntity, insertElementsNested.ToArray(), nestedObject);

            // Then check if any nested object to traverse
            foreach (var nestedProp in nestedProperties)
            {
                if (IsNestedObject(nestedProp))
                {
                    InsertNestedObject(nestedProp, nestedObject, nestedTableName, nestedId);
                }
            }
        }

        private string GenerateInsertStatement(string tableName, PropertyInfo[] properties)
        {
            string columns = string.Join(", ", properties.Select(p => p.Name));
            string values = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            return $"INSERT INTO {tableName} ({columns}) VALUES ({values});";
        }

        private string GenerateNestedInsertStatement(string tableName, PropertyInfo[] properties, string parentTableName, object? parentId)
        {
            string columns = string.Join(", ", properties.Select(p => p.Name));
            string values = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            return $"INSERT INTO {tableName} ({columns},{parentTableName}Id) VALUES ({values},{parentId});";
        }

        private void UpdateNestedObject(PropertyInfo nestedProperty, object entity, string parentTableName, object? parentId)
        {
            var type = nestedProperty.PropertyType;
            var nestedObject = nestedProperty.GetValue(entity);

            if (nestedObject != null)
            {
                if (IsEnumerableType(type))
                {
                    // Handle IEnumerable or List<T> type
                    IEnumerable<object> nestedList = nestedObject as IEnumerable<object>;
                    if (nestedList != null)
                    {
                        foreach (var item in nestedList)
                        {
                            UpdateNestedObjectItem(nestedProperty, item, parentTableName, parentId);
                        }
                    }
                }
                else
                {
                    // Handle single nested object
                    UpdateNestedObjectItem(nestedProperty, nestedObject, parentTableName, parentId);
                }
            }
        }

        private void UpdateNestedObjectItem(PropertyInfo nestedProperty, object nestedObject, string parentTableName, object? parentId)
        {
            var nestedType = nestedObject.GetType();
            var nestedTableName = nestedType.Name;
            PropertyInfo[] nestedProperties = nestedType.GetProperties();

            // Update the nested primitive types
            PropertyInfo nestedIdProp = nestedType.GetProperty("Id");
            var nestedId = nestedIdProp.GetValue(nestedObject);

            var updateElementsNested = new List<PropertyInfo>();
            foreach (var nestedProp in nestedProperties)
            {
                if (!IsNestedObject(nestedProp))
                {
                    updateElementsNested.Add(nestedProp);
                }
            }

            // Create UPDATE statement for the nested entity table
            string updateNestedEntity = GenerateUpdateStatement(nestedTableName, updateElementsNested.ToArray(), nestedIdProp.Name);
            ExecuteNonQuery(updateNestedEntity, updateElementsNested.ToArray(), nestedObject);

            // Then check if any nested object to traverse
            foreach (var nestedProp in nestedProperties)
            {
                if (IsNestedObject(nestedProp))
                {
                    UpdateNestedObject(nestedProp, nestedObject, nestedTableName, nestedId);
                }
            }
        }


        private string GenerateUpdateStatement(string tableName, PropertyInfo[] properties, string idColumnName)
        {
            string updateSet = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));
            return $"UPDATE {tableName} SET {updateSet} WHERE {idColumnName} = @Id;";
        }



        private void ExecuteNonQuery(string query, PropertyInfo[] properties, object entity)
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

        private bool IsEnumerableType(Type type)
        {
            return type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(IEnumerable<>) || type.GetGenericTypeDefinition() == typeof(List<>));
        }

        private bool IsNestedObject(PropertyInfo property)
        {
            return property.PropertyType.IsClass && property.PropertyType != typeof(string);
        }
    }
}
