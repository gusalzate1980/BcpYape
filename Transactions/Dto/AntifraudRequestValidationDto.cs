namespace Transactions.Dto
{
    public class AntifraudRequestValidationDto
    {
        public int ExternalTransactionId { set; get; }
        public DateTime CreatedAt { set; get; }
    }

    public class AddTransferDto
    {
        public string SourceAccountId { set; get; }
        public string TargetAccountId { set; get; }
        public int TransferTypeId { set; get; }
        public int Value { set; get; }
    }

    public class ResponseTransactionAntiFraudDto
    {
        public int Id { set; get; }
        public bool IsFraud { set; get; }
    }
}