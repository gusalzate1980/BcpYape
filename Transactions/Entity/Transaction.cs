using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using Transactions.Dto;

namespace Transactions.Entity
{
    public class Transaction
    {
        private static string kafkaBroker = "localhost:9092";
        private static string topicAntiFraude = "anti-fraude-topic";

        private string _SourceAccountId;
        private string _TargetAccountId;

        private int _TransfertypeId;
        private int _Value;

        private int _TransactionId;
        private DateTime _CreatedDate;

        public Transaction(string sourceAccountId, string targetAccountId, int transfertypeIdint, int value)
        {
            _SourceAccountId = sourceAccountId;
            _TargetAccountId = targetAccountId; 
            _TransfertypeId = transfertypeIdint;    
            _Value = value;
        }

        public void AddTransaction()
        {
            //logica para añadir la transaccion

            var config = new ProducerConfig { BootstrapServers = kafkaBroker };
            using (var producer = new ProducerBuilder<Null, AntifraudRequestValidationDto>(config).Build())
            {
                var message = new Message<Null, AntifraudRequestValidationDto>()
                { 
                    Value = new AntifraudRequestValidationDto() 
                    {
                        CreatedAt = DateTime.UtcNow,
                        ExternalTransactionId = 2
                    } 
                };
                producer.Produce(topicAntiFraude, message);
                Console.WriteLine("Mensaje enviado al servicio AntiFraude.");
            }
        }

        public void UpdateTransaction()
        { }
    }
}
