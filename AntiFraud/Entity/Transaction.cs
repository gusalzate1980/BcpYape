namespace AntiFraud.Entity
{
    public class Transaction
    {
        public int _Value;
        public DateTime _CreateDate;

        public int Value { get { return _Value; } }
        
        public async Task<List<Transaction>> GetTodaySourceTransactionsByTransactionIdAsync(int transactionId)
        {
            return new List<Transaction>();
        }

        public async Task<List<Transaction>> GetTodayTargetTransactionsByTransactionIdAsync(int transactionId)
        {
            return new List<Transaction>();
        }
    }
}
