using AntiFraud.Dto;
using Confluent.Kafka;

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

        private static string kafkaBroker = "localhost:9092"; // Dirección del broker de Kafka
        private static string topicTransacciones = "transacciones-topic";

        public TransactionRule(int transactionId,int transactionValue)
        { 
            _TransactionId = transactionId;
            //obtiene valores de las reglas
        }

        private async void SetTransactionValues()
        {
            Transaction transaction = new Transaction();
            Task<List<Transaction>> taskSources =  Task.Run(()=>transaction.GetTodaySourceTransactionsByTransactionIdAsync(_TransactionId));
            Task<List<Transaction>> taskTargets =  Task.Run(()=>transaction.GetTodayTargetTransactionsByTransactionIdAsync(_TransactionId));

            _sources = await taskSources;
            _Targets = await taskTargets;
        }

        

        public TransactionStatusDto TransactionListener()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = kafkaBroker,
                GroupId = "grupo-anti-fraude",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe("anti-fraude-topic");

                while (true)
                {
                    var consumeResult = consumer.Consume();
                    string transaccion = consumeResult.Message.Value;

                    this.SendValidationResult(this.IsFraud());

                    
                }
            }
        }

        private void SendValidationResult(TransactionStatusDto resultado)
        {
            var config = new ProducerConfig { BootstrapServers = kafkaBroker };
            using (var producer = new ProducerBuilder<Null, TransactionStatusDto>(config).Build())
            {
                var message = new Message<Null, TransactionStatusDto> { Value = resultado };
                producer.Produce(topicTransacciones, message);
            }
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
