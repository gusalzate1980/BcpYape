using DataBase;
using Transactions.Dao;

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

        public ITransactionDao CreateTransactionDao()
        {
            switch (_engine)
            {
                case DataBaseEngine.SqlServer: return new TransactionSqlServerDao(this.CreateDataBaseEngine());
                default: return new TransactionSqlServerDao(this.CreateDataBaseEngine());
            }
        }
    }

    public enum DataBaseEngine
    {
        SqlServer,
        Oracle
    }
}
