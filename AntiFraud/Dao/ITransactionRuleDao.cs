using AntiFraud.Dto;

namespace AntiFraud.Dao
{
    public interface ITransactionRuleDao
    {
        public RulesDto GetTransactionsRuleValues();
    }
}
