using AFORO255.MS.TEST.Pay.DTO;
using AFORO255.MS.TEST.Pay.Service;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AFORO255.MS.TEST.Pay.Controllers
{
    [Route("api/Transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        [HttpGet]
        public IActionResult GetTransaction()
        {
            var transaction = _transactionService.GetAll();

            return Ok(transaction);
        }
        [HttpPost("Pago")]
        public IActionResult Pagos([FromBody] TransactionRequest request)
        {
            Model.Transaction transaction = new Model.Transaction()
            {
                IdInvoice = request.IdInvoice,
                Amount = request.Amount,
                Date = DateTime.Now.ToString()
            };
            _transactionService.Pay(transaction);

            return Ok(transaction);
        }
    }
}
