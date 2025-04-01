using System;
using System.Collections.Generic;
using System.Data;
using DataBase;
using Microsoft.Data.SqlClient;

public class SqlServer : IDataBase
{
    private SqlConnection _connection;
    private bool _CommandExecuted;
    private string _WhyCommandFailed;

    public SqlServer(string connectionString)
    {
        this._connection = new SqlConnection(connectionString);
    }

    public void OpenConnection()
    {
        if (this._connection.State != ConnectionState.Open)
        {
            this._connection.Open();
        }
    }

    public void CloseConnection()
    {
        if (this._connection.State != ConnectionState.Closed)
        {
            this._connection.Close();
        }
    }

    public List<T> ExecuteTableQuery<T>(string query) where T : new()
    {
        List<T> results = new List<T>();

        using (SqlCommand command = new SqlCommand(query, this._connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    T item = new T();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        // Asigna los valores del lector a las propiedades del objeto
                        var property = typeof(T).GetProperty(reader.GetName(i));
                        if (property != null && !reader.IsDBNull(i))
                        {
                            property.SetValue(item, reader.GetValue(i));
                        }
                    }
                    results.Add(item);
                }
            }
        }

        return results;
    }

    public T ExecuteScalarQuery<T>(string query, Dictionary<string, object> parameters)
    {
        using (SqlCommand command = new SqlCommand(query, this._connection))
        {
            // Agregar parámetros a la consulta
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                }
            }

            var result = command.ExecuteScalar();
            if (result == null || result == DBNull.Value)
            {
                return default(T);
            }
            return (T)Convert.ChangeType(result, typeof(T));
        }
    }

    public void ExecuteNonQuery(string command, Dictionary<string, object> parameters)
    {
        using (SqlCommand sqlCommand = new SqlCommand(command, this._connection))
        {
            // Agregar parámetros a la consulta
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    sqlCommand.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                }
            }

            sqlCommand.ExecuteNonQuery();
        }
    }

    public void Dispose()
    {
        CloseConnection();
        this._connection.Dispose();
    }

    public List<T> ExecuteTableQuery<T>(string query, DataTable dataTable, string dataTableParameterName) where T : new()
    {
        List<T> results = new List<T>();

        using (SqlCommand command = new SqlCommand(query, this._connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                SqlParameter parameter = command.Parameters.AddWithValue("@" + dataTableParameterName, dataTable);
                parameter.SqlDbType = SqlDbType.Structured;

                while (reader.Read())
                {
                    T item = new T();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        // Asigna los valores del lector a las propiedades del objeto
                        var property = typeof(T).GetProperty(reader.GetName(i));
                        if (property != null && !reader.IsDBNull(i))
                        {
                            property.SetValue(item, reader.GetValue(i));
                        }
                    }
                    results.Add(item);
                }
            }
        }

        return results;
    }
}