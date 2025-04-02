using Confluent.Kafka;
using Transactions.Dto;
using static Confluent.Kafka.ConfigPropertyNames;

namespace Transactions.Kafka
{
    public class TransactionKafka
    {
        private static string _kafkaBroker = "localhost:9092";
        private static string _transactionToAntiFraudTopic = "transaction-to-anti-fraude-topic";
        private static string _fromAntiFraudToTransactionTopic = "anti-fraud-to-transaction-topic";

        public void SendTransaction(int transactionId)
        {
            var config = new ProducerConfig { BootstrapServers = _kafkaBroker };
            using (var producer = new ProducerBuilder<Null, AntifraudRequestValidationDto>(config).Build())
            {
                var message = new Message<Null, AntifraudRequestValidationDto>()
                {
                    Value = new AntifraudRequestValidationDto()
                    {
                        CreatedAt = DateTime.UtcNow,
                        ExternalTransactionId = transactionId,
                    }
                };
                producer.Produce(_transactionToAntiFraudTopic, message);
            }
        }

        public void StartListeningForAntiFraudResponses(Action<ResponseTransactionAntiFraudDto> onMessageReceived)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _kafkaBroker,
                GroupId = "anti_fraud_group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Null, ResponseTransactionAntiFraudDto>(config).Build())
            {
                consumer.Subscribe(_fromAntiFraudToTransactionTopic);

                Task.Run(() =>
                {
                    while (true)
                    {
                        try
                        {
                            var cr = consumer.Consume();
                            ResponseTransactionAntiFraudDto response = cr.Value;

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

    }
}
