using AntiFraud.Dao;
using AntiFraud.Dto;

namespace AntiFraud.Entity
{
    public class TransactionRule
    {
        private int _IndividualMaxAmount;
        private int _DailyMaxAmount;
        private int _TransactionId;
        private bool _IsFraud;

        private List<Transaction> _sources;
        private List<Transaction> _Targets;
        private int _TransactionValue;

        private ITransactionRuleDao _dao;

        public TransactionRule(int transactionId,int transactionValue,ITransactionRuleDao dao)
        { 
            _TransactionId      = transactionId;
            _TransactionValue   = transactionValue;
            _dao =   dao;

            var rules = _dao.GetTransactionsRuleValues();
            _IndividualMaxAmount = rules.IndividualMaxAmount;
            _DailyMaxAmount = rules.DailyMaxAmount;
        }

        private async void SetTransactionValues()
        {
            Transaction transaction = new Transaction();
            Task<List<Transaction>> taskSources =  Task.Run(()=>transaction.GetTodaySourceTransactionsByTransactionIdAsync(_TransactionId));
            Task<List<Transaction>> taskTargets =  Task.Run(()=>transaction.GetTodayTargetTransactionsByTransactionIdAsync(_TransactionId));

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
