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
        private readonly TransactionRuleKafka _kafka;

        public AntiFraudBl(TransactionRuleKafka kafka)
        {
            _kafka = kafka;
            _factory = new TransactionRuleFactory(DataBaseEngine.SqlServer);
            StartListeningFromTransactionMessages();
        }

        private void StartListeningFromTransactionMessages()
        {
            _kafka.StartListeningFromTransactionMessages(response =>
            {
                _rule = new TransactionRule(response.ExternalTransactionId, response.CreatedAt, _factory.CreateTransactionRuleDao());
                var validationResult = this.IsFraud();
                _kafka.SendValidatedTransactionResult(validationResult);
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