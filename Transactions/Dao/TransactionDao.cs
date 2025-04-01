using Transactions.Factory;

namespace Transactions.Dao
{
    public class TransactionDao : ITransactionDao
    {
        ITransactionDao _dao;

        public TransactionDao(ITransactionDao dao)
        {
            _dao = dao;
        }

        public int AddTransaction(string sourceAccountId, string targetAccountId, int transfertypeIdint, int value)
        {
            
        }

        public void UpdaterTransaction(int transactionId)
        {
            throw new NotImplementedException();
        }
    }
}
