using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Transactions.BusinessLogic;
using Transactions.Dto;
using Transactions.Kafka;

namespace Transactions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionKafka _kafka;

        public TransactionController(TransactionKafka kafka)
        {
            _kafka = kafka;
        }

        [HttpPost]
        public IActionResult AddTransaction(AddTransferDto dto)
        {
            TransactionBl transactionBl = new TransactionBl(_kafka);
            return Ok(transactionBl.AddTransaction(dto));
        }
    }
}
