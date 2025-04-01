using Confluent.Kafka;
using Transactions.Dao;
using Transactions.Dto;

namespace Transactions.Entity
{
    public class Transaction
    {
        private string _SourceAccountId;
        private string _TargetAccountId;

        private int _TransfertypeId;
        private int _Value;

        private string _status;

        private int _TransactionId;

        public int TransactionId { get { return _TransactionId; } }    

        private DateTime _CreatedDate;

        private ITransactionDao _dao;

        public Transaction(string sourceAccountId, string targetAccountId, int transfertypeIdint, int value, ITransactionDao dao)
        {
            _SourceAccountId = sourceAccountId;
            _TargetAccountId = targetAccountId; 
            _TransfertypeId = transfertypeIdint;    
            _Value = value;

            _dao = dao;
        }

        public Transaction(ITransactionDao dao,int transactionId,bool isFraud)
        {
            _dao = dao;
            _TransactionId = transactionId;
            _status = isFraud ? "C":"A";
        }

        public void AddTransaction()
        {
            _TransactionId  =   _dao.AddTransaction(_SourceAccountId, _TargetAccountId, _TransfertypeId, _Value);
        }

        public void UpdateTransaction()
        {
            _dao.UpdateTransaction(_TransactionId, _status);
        }
    }
}