using AntiFraud.Dto;
using Confluent.Kafka;

namespace AntiFraud.Kafka
{
    public class TransactionRuleKafka
    {
        private static string _kafkaBroker = "localhost:9092";
        private static string _transactionToAntiFraudTopic = "transaction-to-anti-fraude-topic";
        private static string _fromAntiFraudToTransactionTopic = "anti-fraud-to-transaction-topic";

        public void StartListeningFromTransactionMessages(Action<AntifraudRequestValidationDto> onMessageReceived)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _kafkaBroker,
                GroupId = "anti_fraud_group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Null, AntifraudRequestValidationDto>(config).Build())
            {
                consumer.Subscribe(_transactionToAntiFraudTopic);

                Task.Run(() =>
                {
                    while (true)
                    {
                        try
                        {
                            var cr = consumer.Consume();
                            AntifraudRequestValidationDto response = cr.Value;

                            onMessageReceived(response);
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Error al consumir mensaje: {e.Error.Reason}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                    }
                });
            }
        }

        public void SendValidatedTransactionResult(TransactionStatusDto result)
        {
            var config = new ProducerConfig { BootstrapServers = _kafkaBroker };
            using (var producer = new ProducerBuilder<Null, TransactionStatusDto>(config).Build())
            {
                var message = new Message<Null, TransactionStatusDto>()
                {
                    Value = result
                };
                producer.Produce(_fromAntiFraudToTransactionTopic, message);
            }
        }
    }
}
