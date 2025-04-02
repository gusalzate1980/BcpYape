using AntiFraud.Entity;
using DataBase;

namespace AntiFraud.Dao
{
    public class TransactionSqlServerDao : ITransactionDao
    {
        IDataBase _dataBase;

        public TransactionSqlServerDao(IDataBase dataBase)
        {
            _dataBase = dataBase;
        }

        public List<int> GetSourceTransactions(int transactionId, DateTime createdAt)
        {
            _dataBase.OpenConnection();

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("@TransactionId", transactionId);
            data.Add("@CreatedAt", createdAt);

            List<int> transactions = _dataBase.ExecuteTableQuery<int>("EXEC GetSourceTransactions @TransactionId, @createdAt",data);

            _dataBase.CloseConnection();

            return transactions;
        }

        public List<int> GetTargetTransactions(int transactionId, DateTime createdAt)
        {
            _dataBase.OpenConnection();

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("@TransactionId", transactionId);
            data.Add("@CreatedAt", createdAt);

            List<int> transactions = _dataBase.ExecuteTableQuery<int>("EXEC GetTargetTransactions @TransactionId, @createdAt",data);

            _dataBase.CloseConnection();

            return transactions;
        }
    }
}
