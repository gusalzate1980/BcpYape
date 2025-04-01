using Confluent.Kafka;
using Transactions.Dto;
using Transactions.Entity;
using Transactions.Factory;
using Transactions.Kafka;

namespace Transactions.BusinessLogic
{
    public class TransactionBl
    {
        private Transaction _transaction;
        private TransactionFactory _factory;

        public TransactionBl() 
        {
            _factory = new TransactionFactory(DataBaseEngine.SqlServer);
            StartListeningForFraudResponses();
        }

        public int AddTransaction(AddTransferDto dto)
        {
            
            _transaction = new Transaction(dto.SourceAccountId, dto.TargetAccountId, dto.TransferTypeId, dto.Value, _factory.CreateTransactionDao());

            _transaction.AddTransaction();

            TransactionKafka.SendTransaction(_transaction.TransactionId);

            return _transaction.TransactionId;
        }

        private void StartListeningForFraudResponses()
        {
            TransactionKafka.StartListeningForAntiFraudResponses(response => 
            {
                this.UpdateTransaction(response);
            });
        }

        public void UpdateTransaction(ResponseTransactionAntiFraudDto dto)
        {
            _transaction = new Transaction(_factory.CreateTransactionDao(),dto.Id,dto.IsFraud);

        }
    }
}