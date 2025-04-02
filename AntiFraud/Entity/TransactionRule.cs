using AntiFraud.Dao;
using AntiFraud.Dto;
using AntiFraud.Factory;

namespace AntiFraud.Entity
{
    public class TransactionRule
    {
        private int _IndividualMaxAmount;
        private int _DailyMaxAmount;
        private int _TransactionId;
        public int TransactionId { get { return _TransactionId; } }

        private bool _IsFraud;
        private DateTime _TransactionDate;

        private List<Transaction> _sources;
        private List<Transaction> _Targets;
        private int _TransactionValue;

        private ITransactionRuleDao _dao;
        private TransactionRuleFactory _factory;

        public TransactionRule(int transactionId,DateTime createAt,ITransactionRuleDao dao)
        { 
            _TransactionId      = transactionId;
            _TransactionDate   = createAt;
            _dao =   dao;

            _factory = new TransactionRuleFactory(DataBaseEngine.SqlServer);

            var rules = _dao.GetTransactionsRuleValues();
            _IndividualMaxAmount = rules.IndividualMaxAmount;
            _DailyMaxAmount = rules.DailyMaxAmount;
        }

        private async void SetTransactionValues()
        {
            Transaction transaction = new Transaction(_TransactionId,_factory.CreateTransactionDao());
            transaction.CreateDate = _TransactionDate;

            Task<List<Transaction>> taskSources =  Task.Run(()=>transaction.GetSourceTransactions());
            Task<List<Transaction>> taskTargets =  Task.Run(()=>transaction.GetTargetTransactions());

            _sources = await taskSources;
            _Targets = await taskTargets;
        }

        public TransactionStatusDto IsFraud()
        {
            this.SetTransactionValues();

            if (_sources.Sum(s => s.Value) > _DailyMaxAmount || _IndividualMaxAmount > _TransactionValue
                || _Targets.Sum(s => s.Value) > _DailyMaxAmount)
            {
                return new TransactionStatusDto()
                {
                    IsApproved = false,
                    TransactionId = _TransactionId
                };
            }
            else
            {
                return new TransactionStatusDto()
                {
                    IsApproved = true,
                    TransactionId = _TransactionId
                };
            }
        }

    }
}