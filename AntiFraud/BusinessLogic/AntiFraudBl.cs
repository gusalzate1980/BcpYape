using AntiFraud.Entity;

namespace AntiFraud.BusinessLogic
{
    public class AntiFraudBl
    {
        public  TransactionRule     _rule;
        private TransactionFactory  _factory;

        public TransactionBl()
        {
            _factory = new TransactionFactory(DataBaseEngine.SqlServer);
            StartListeningFromTransactionMessages();
        }

        private void StartListeningFromTransactionMessages()
        {
            TransactionKafka.StartListeningFromTransactionMessages(response =>
            {
                this.Is(response);
            });
        }

        private ResponseTransactionAntiFraudDto IsFraud()
        {
            _rule.
        }
    }
}
