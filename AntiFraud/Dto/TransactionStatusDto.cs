namespace AntiFraud.Dto
{
    public class TransactionStatusDto
    {
        public int TransactionId { set; get; }
        public bool IsApproved { set; get; }
    }

    public class RulesDto
    {
        public int IndividualMaxAmount {  set; get; }   
        public int DailyMaxAmount { set; get; }
    }

    public class ResponseTransactionAntiFraudDto
    {
        public int Id { set; get; }
        public bool IsFraud { set; get; }
    }
    public class AntifraudRequestValidationDto
    {
        public int ExternalTransactionId { set; get; }
        public DateTime CreatedAt { set; get; }
    }

}