using AntiFraud.Dto;
using AntiFraud.Entity;
using AntiFraud.Factory;
using AntiFraud.Kafka;

namespace AntiFraud.BusinessLogic
{
    public class AntiFraudBl
    {
        TransactionRuleFactory _factory;
        TransactionRule _rule; 
        
        public AntiFraudBl()
        {
            _factory = new TransactionRuleFactory(DataBaseEngine.SqlServer);
            StartListeningFromTransactionMessages();
        }

        private void StartListeningFromTransactionMessages()
        {
            TransactionRuleKafka.StartListeningFromTransactionMessages(response =>
            {
                _rule = new TransactionRule(response.ExternalTransactionId, response.CreatedAt, _factory.CreateTransactionRuleDao())
                this.IsFraud();
            });
        }

        private ResponseTransactionAntiFraudDto IsFraud()
        {
            var status = _rule.IsFraud();

            return new ResponseTransactionAntiFraudDto()
            {
                Id = _rule.TransactionId,
                IsFraud = !status.IsApproved
            };
        }
    }
}