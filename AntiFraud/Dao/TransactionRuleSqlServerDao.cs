using AntiFraud.Dto;
using DataBase;
using System.Collections.Generic;

namespace AntiFraud.Dao
{
    public class TransactionRuleSqlServerDao : ITransactionRuleDao
    {
        IDataBase _dataBase;

        public TransactionRuleSqlServerDao(IDataBase dataBase)
        {
            _dataBase = dataBase;
        }
        public RulesDto GetTransactionsRuleValues()
        {
            _dataBase.OpenConnection();
            RulesDto rules =  _dataBase.ExecuteTableQuery<RulesDto>("EXEC GetTransactionRules").FirstOrDefault();
            _dataBase.CloseConnection();

            return rules;
        }
    }
}