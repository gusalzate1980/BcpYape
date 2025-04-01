using DataBase;

namespace Transactions.Dao
{
    public class TransactionSqlServerDao : ITransactionDao
    {
        IDataBase _dataBase;

        public TransactionSqlServerDao(IDataBase dataBase)
        {
            _dataBase = dataBase;
        }

        public int AddTransaction(string sourceAccountId, string targetAccountId, int transfertypeId, int value)
        {
            _dataBase.OpenConnection();

            Dictionary<string,object> data = new Dictionary<string,object>();
            data.Add("sourceAccountId", sourceAccountId);
            data.Add("targetAccountId", targetAccountId);
            data.Add("transfertypeIdint", transfertypeId);
            data.Add("value", value);

            int id = _dataBase.ExecuteScalarQuery<int>("EXEC AddTransaction @sourceAccountId,@targetAccountId,@transfertypeId,@value", data);
            
            _dataBase.CloseConnection();

            return id;
        }

        public void UpdateTransaction(int transactionId,string status)
        {
            _dataBase.OpenConnection();

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("transactionId", transactionId);
            data.Add("status", status);

            int id = _dataBase.ExecuteScalarQuery<int>("EXEC UpdateTransaction @transactionId, @status", data);

            _dataBase.CloseConnection();
        }
    }
}