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
        public List<T> ExecuteTableQuery<T>(string query, Dictionary<string, object> parameters) where T : new();

       T ExecuteScalarQuery<T>(string query, Dictionary<string, object> parameters);

        void ExecuteNonQuery(string command, Dictionary<string, object> parameters);
    }
}