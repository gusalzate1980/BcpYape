using AntiFraud.Dao;
using DataBase;

namespace Transactions.Factory
{
    public class TransactionFactory
    {

        private DataBaseEngine _engine;
        public TransactionFactory(DataBaseEngine engine)
        {
            _engine = engine;
        }

        public IDataBase CreateDataBaseEngine()
        {
            switch (_engine)
            {
                case DataBaseEngine.SqlServer: return new SqlServer("Server=CO-IT003269\\SQLEXPRESS;Database=BcpYape;User Id=sa;Password=$qlS3erver;TrustServerCertificate=True");
                default: return new SqlServer("Server=CO-IT003269\\SQLEXPRESS;Database=BcpYape;User Id=sa;Password=$qlS3erver;TrustServerCertificate=True");
            }
        }

        public ITransactionRuleDao CreateTransactionRuleDao()
        {
            switch (_engine)
            {
                case DataBaseEngine.SqlServer: return new TransactionRuleSqlServerDao(this.CreateDataBaseEngine());
                default: return new TransactionRuleSqlServerDao(this.CreateDataBaseEngine());
            }
        }
    }

    public enum DataBaseEngine
    {
        SqlServer,
        Oracle
    }
}
