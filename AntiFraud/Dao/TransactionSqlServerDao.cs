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

        public List<Transaction> GetSourceTransactions(int transactionId, DateTime createdAt)
        {
            _dataBase.OpenConnection();

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("@TransactionId", transactionId);
            data.Add("@CreatedAt", createdAt);

            List<Transaction> transactions = _dataBase.ExecuteTableQuery<Transaction>("EXEC GetSourceTransactions @TransactionId, @createdAt");

            _dataBase.CloseConnection();

            return transactions;
        }

        public List<Transaction> GetTargetTransactions(int transactionId, DateTime createdAt)
        {
            _dataBase.OpenConnection();

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("@TransactionId", transactionId);
            data.Add("@CreatedAt", createdAt);

            List<Transaction> transactions = _dataBase.ExecuteTableQuery<Transaction>("EXEC GetTargetTransactions @TransactionId, @createdAt");

            _dataBase.CloseConnection();

            return transactions;
        }
    }
}
