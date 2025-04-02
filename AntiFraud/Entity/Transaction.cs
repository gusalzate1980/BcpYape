using AntiFraud.Dao;

namespace AntiFraud.Entity
{
    public class Transaction
    {
        private int _Value;
        public DateTime CreateDate;
        private int _TransactionId;
        private ITransactionDao _dao;


        public int Value { get { return _Value; } }
        
        public Transaction(int transactionId,ITransactionDao dao)
        {
            _TransactionId = transactionId;
            _dao = dao;
        }

        public List<Transaction> GetSourceTransactions()
        {
            return new List<Transaction>();
        }

        public List<Transaction> GetTargetTransactions()
        {
            return new List<Transaction>();
        }
    }
}
