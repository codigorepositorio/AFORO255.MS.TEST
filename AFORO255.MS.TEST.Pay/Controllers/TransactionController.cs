using AFORO255.MS.TEST.Pay.DTO;
using AFORO255.MS.TEST.Pay.RabbitMQ.Commands;
using AFORO255.MS.TEST.Pay.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MS.AFORO255.Cross.Metrics.Registry;
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
        private readonly IMetricsRegistry _metricsRegistry;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(IInvoiceService invoiceService, IEventBus bus, ITransactionService transactionService, IMetricsRegistry metricsRegistry, ILogger<TransactionController> logger)
        {
            _invoiceService = invoiceService;
            _bus = bus;
            _transactionService = transactionService;
            _metricsRegistry = metricsRegistry;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetTransaction()
        {
            var transaction = _invoiceService.GetAll();

            return Ok(transaction);
        }
        [HttpPost("Pay")]
        public IActionResult Pagos([FromBody] InvoiceRequest request)
        {
            _metricsRegistry.IncrementFindQuery();
            _logger.LogInformation("Post Pay(Pagos)");

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
                var createCommad = new TransactionPayCreatedCommand(
                idOperation: transaction.IdOperation,               
                amount: transaction.Amount,
                date: transaction.Date,
                 idInvoice: transaction.IdInvoice
                );
                _bus.SendCommand(createCommad);

                var createdInvoice = new InvoiceCreatedCommand(
                       idInvoice: transaction.IdInvoice,
                       amount: transaction.Amount,
                       state: "PAGADO"
                    );

                _bus.SendCommand(createdInvoice);

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
