using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Assignment3
{
    public class MyORM<G, T> where T : class
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

            var deleteQuery = GenerateNestedDeleteStatement(nestedTableName,nestedId, parentTableName, parentId);
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

        private string GenerateNestedDeleteStatement(string nestedTableName,object? nestedTableId, string parentTableName, object? parentId)
        {
            return $"DELETE FROM [{nestedTableName}] WHERE Id='{nestedTableId}' And [{parentTableName}Id]='{parentId}';";
        }

        private string GenerateDeleteStatement(string tableName, object Id)
        {
            return $"DELETE FROM [{tableName}] WHERE Id='{Id}';";
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

            return $"INSERT INTO [{tableName}] ({columns}) VALUES ({values});";
        }

        private string GenerateNestedInsertStatement(string tableName, PropertyInfo[] properties, string parentTableName, object? parentId)
        {
            string columns = string.Join(", ", properties.Select(p => p.Name));
            string values = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            if (parentId is Guid)
            {
                // If parentId is a Guid, surround it with single quotes
                return $"INSERT INTO [{tableName}] ({columns},[{parentTableName}Id]) VALUES ({values},'{parentId}');";
            }
            else
            {
                // If parentId is not a Guid, assume it's already formatted correctly
                return $"INSERT INTO [{tableName}] ({columns},[{parentTableName}Id]) VALUES ({values},{parentId});";
            }
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
            return $"UPDATE [{tableName}] SET {updateSet} WHERE {idColumnName} = @Id;";
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



        public T GetById(G id)
        {
            string tableName = typeof(T).Name;
            string query = $"SELECT * FROM [{tableName}] WHERE Id=@Id;";

            T result = Activator.CreateInstance<T>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Map primitive properties
                            result = MapPrimitiveProperties(reader);

                            // Map nested objects
                            MapNestedObjects(result,reader);
                        }
                    }
                }
            }

            return result;
        }

        private void MapNestedObjects(object result, SqlDataReader reader)
        {
            Type entityType = result.GetType();
            PropertyInfo[] properties = entityType.GetProperties();
            string parentTableName = entityType.Name;

            foreach (PropertyInfo property in properties)
            {
                if (IsEnumerableType(property.PropertyType))
                {
                    var list = (IList)Activator.CreateInstance(property.PropertyType);
                    Type listType = property.PropertyType.GetGenericArguments()[0];

                    // Construct SQL query to retrieve nested objects
                    string childTableName = listType.Name;
                    string query = $"SELECT * FROM [{childTableName}] WHERE [{parentTableName}Id]=@Id;";

                    // Log the constructed query
                    Console.WriteLine($"Constructed Query: {query}");

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (SqlCommand childCommand = new SqlCommand(query, connection))
                        {
                            childCommand.Parameters.AddWithValue("@Id", reader["Id"]);

                            using (SqlDataReader childReader = childCommand.ExecuteReader())
                            {
                                while (childReader.Read())
                                {
                                    // Log the data read from the childReader
                                    for (int i = 0; i < childReader.FieldCount; i++)
                                    {
                                        Console.WriteLine($"{childReader.GetName(i)}: {childReader.GetValue(i)}");
                                    }

                                    var nestedObject = MapPrimitiveProperties(childReader, listType);
                                    MapNestedObjects(nestedObject, childReader); // Recursively map nested objects
                                    list.Add(nestedObject);
                                }
                            }
                        }
                    }

                    // Set the populated list to the property
                    property.SetValue(result, list);
                }
                else if (IsNestedObject(property))
                {
                    object nestedObject = Activator.CreateInstance(property.PropertyType);
                    var childTableName = property.PropertyType.Name;
                    string query = $"SELECT * FROM [{childTableName}] WHERE [{parentTableName}Id]=@Id;";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (SqlCommand childCommand = new SqlCommand(query, connection))
                        {
                            childCommand.Parameters.AddWithValue("@Id", reader["Id"]);

                            using (SqlDataReader Reader = childCommand.ExecuteReader())
                            {
                                while (Reader.Read())
                                {
                                    // Log the data read from the childReader
                                    for (int i = 0; i < Reader.FieldCount; i++)
                                    {
                                        Console.WriteLine($"{Reader.GetName(i)}: {Reader.GetValue(i)}");
                                    }

                                    nestedObject = MapPrimitiveProperties(Reader, property.PropertyType);
                                    MapNestedObjects(nestedObject, Reader); // Recursively map nested objects
                                }
                            }
                        }
                    }
                    property.SetValue(result, nestedObject);
                }
            }
        }




        private object MapPrimitiveProperties(SqlDataReader reader, Type objectType)
        {
            object result = Activator.CreateInstance(objectType);
            PropertyInfo[] properties = objectType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (!IsNestedObject(property))
                {
                    string columnName = ToPascalCase(property.Name);
                    int columnIndex = reader.GetOrdinal(columnName);
                    if (columnIndex != -1 && !reader.IsDBNull(columnIndex))
                    {
                        object value = reader[columnIndex] == DBNull.Value ? null : reader[columnIndex];
                        property.SetValue(result, value);
                    }
                }
            }

            return result;
        }



        private T MapPrimitiveProperties(SqlDataReader reader)
        {
            Type entityType = typeof(T);
            T result = Activator.CreateInstance<T>();

            PropertyInfo[] properties = entityType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (!IsNestedObject(property))
                {
                    string columnName = ToPascalCase(property.Name);
                    if (!reader.IsDBNull(columnName))
                    {
                        object value = reader[columnName] == DBNull.Value ? null : reader[columnName];
                        property.SetValue(result, value);
                    }
                }
            }

            return result;
        }

        private string ToPascalCase(string str)
        {
            return char.ToUpper(str[0]) + str.Substring(1);
        }


        public void Delete(G id)
        {
            var Object = GetById(id);
            if (Object != null)
            {
                Delete(Object);
            }
        }


        public List<T> GetAll()
        {
            var list = new List<T>();

            var idList = new List<G>();

            var tableName = typeof(T).Name;

            string query = $"SELECT [ID]  FROM [{tableName}];";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand Command = new SqlCommand(query, connection))
                {

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            idList.Add((G)Reader["Id"]);
                        } 
                    }
                }
            }

            foreach (var id in idList)
            {
                var obj = GetById(id);
                list.Add(obj);
            }

            return list;
        }

        private SqlDbType GetSqlDbType(Type type)
        {
            if (type == typeof(string))
            {
                return SqlDbType.NVarChar;
            }
            else if (type == typeof(int))
            {
                return SqlDbType.Int;
            }
            else if (type == typeof(double))
            {
                return SqlDbType.Float;
            }
            else if (type == typeof(Guid))
            {
                return SqlDbType.UniqueIdentifier;
            }
            else
            {
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