using AntiFraud.Entity;

namespace AntiFraud.Dao
{
    public interface ITransactionDao
    {
        public List<int> GetSourceTransactions(int transactionId,DateTime createdAt);
        public List<int> GetTargetTransactions(int transactionId, DateTime createdAt);
    }
}
