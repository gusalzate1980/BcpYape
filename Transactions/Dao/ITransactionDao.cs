namespace Transactions.Dao
{
    public interface ITransactionDao
    {
        public int AddTransaction(string sourceAccountId, string targetAccountId, int transfertypeIdint, int value);
        public void UpdateTransaction(int transactionId,string status);
    }
}