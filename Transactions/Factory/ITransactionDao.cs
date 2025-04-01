namespace Transactions.Factory
{
    public interface ITransactionDao
    {
        public int AddTransaction(string sourceAccountId, string targetAccountId, int transfertypeIdint, int value);
        public void UpdaterTransaction(int transactionId);
    }
}