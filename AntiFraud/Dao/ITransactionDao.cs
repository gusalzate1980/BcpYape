using AntiFraud.Entity;

namespace AntiFraud.Dao
{
    public interface ITransactionDao
    {
        public List<Transaction> GetSourceTransactions(int transactionId,DateTime createdAt);
        public List<Transaction> GetTargetTransactions(int transactionId, DateTime createdAt);
    }
}
