using AFORO255.MS.TEST.Pay.DTO;
using AFORO255.MS.TEST.Pay.RabbitMQ.Commands;
using AFORO255.MS.TEST.Pay.Service;
using Microsoft.AspNetCore.Mvc;
using MS.AFORO255.Cross.RabbitMQ.Src.Bus;
using System;
using System.Linq;

namespace AFORO255.MS.TEST.Pay.Controllers
{
    [Route("api/Transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IEventBus _bus;
        private readonly ITransactionService _transactionService;

        public TransactionController(IInvoiceService invoiceService, IEventBus bus, ITransactionService transactionService)
        {
            _invoiceService = invoiceService;
            _bus = bus;
            _transactionService = transactionService;
        }
        [HttpGet]
        public IActionResult GetTransaction()
        {
            var transaction = _invoiceService.GetAll();

            return Ok(transaction);
        }
        [HttpPost("Pago")]
        public IActionResult Pagos([FromBody] InvoiceRequest request)
        {

            Model.Transaction transaction = new Model.Transaction()
            {
                IdInvoice = request.IdInvoice,
                Amount = request.Amount,
                Date = DateTime.Now.ToString()
            };
            transaction = _transactionService.Pay(transaction);

            bool isProccess = _invoiceService.Execute(transaction);

            if (isProccess)
            {
                var createCommad = new PayCreatedCommand(
                idOperation: transaction.IdOperation,               
                amount: transaction.Amount,
                date: transaction.Date,
                 idInvoice: transaction.IdInvoice
                );
                _bus.SendCommand(createCommad);


                //var createdInvoice = new InvoiceCreatedCommand(
                //       idInvoice: transaction.IdInvoice,
                //       amount: transaction.Amount,
                //       state: "PAGADO"
                //    );

                //_bus.SendCommand(createdInvoice);

                var createCommadNotification = new NotificationCreateCommand(
                idOperation: transaction.IdOperation,
                idInvoice: transaction.IdInvoice,
                amount: transaction.Amount);

                _bus.SendCommand(createCommadNotification);

            }

            return Ok(transaction);
        }
    }
}
