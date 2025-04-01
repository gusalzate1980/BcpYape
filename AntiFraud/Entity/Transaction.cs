namespace AntiFraud.Entity
{
    public class Transaction
    {
        private int _Value;
        public DateTime CreateDate;
        private int _TransactionId;

        public int Value { get { return _Value; } }
        
        public Transaction(int transactionId)
        {
            _TransactionId = transactionId;
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
