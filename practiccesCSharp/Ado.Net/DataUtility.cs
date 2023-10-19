using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado.Net
{
    public class DataUtility
    {
        private readonly string _connectionString;

        public DataUtility(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int WriteOperation(string commandText, Dictionary<string, object> parameterMap)
        {
            using SqlCommand command = GetCommand(commandText,parameterMap);

            int count = command.ExecuteNonQuery();

            return count;
        }

        public List<object[]> ReadOperation(string commandText,Dictionary<string, object> parameterMap)
        {
            using SqlCommand command = GetCommand(commandText, parameterMap);

            using SqlDataReader reader = command.ExecuteReader();

            List<object[]> data = new List<object[]>();

            while (reader.Read())
            {
                object[] row = new object[reader.FieldCount];

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[i] = reader.GetValue(i);
                }

                data.Add(row);
            }

            return data;
        }

        private SqlCommand GetCommand(string commandText, Dictionary<string, object> parameterMap)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand(commandText, connection);

            if (command.Connection.State != System.Data.ConnectionState.Open)
                command.Connection.Open();
            if(parameterMap != null)
            {
                foreach(var p in parameterMap)
                {
                    command.Parameters.Add(new SqlParameter(p.Key, p.Value));
                }
            }
            return command;
        }

    }
}
