using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public interface IDataBase : IDisposable
    {
        void OpenConnection();
        void CloseConnection();
        List<T> ExecuteTableQuery<T>(string query) where T : new();
        List<T> ExecuteTableQuery<T>(string query, DataTable dataTable, string dataTableParameterName) where T : new();
        T ExecuteScalarQuery<T>(string query);

        void ExecuteNonQuery(string query);
    }
}