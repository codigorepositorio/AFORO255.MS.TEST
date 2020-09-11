using System.Linq;
using AFORO255.MS.TEST.Invoice.DTO;
using AFORO255.MS.TEST.Invoice.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MS.AFORO255.Cross.Metrics.Registry;

namespace AFORO255.MS.TEST.Invoice.Controllers
{
    [Route("api/Invoice")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IMetricsRegistry _metricsRegistry;
        private readonly IInvoiceService _invoiceService;
        private readonly ILogger<InvoiceController> _logger;

        public InvoiceController(IMetricsRegistry metricsRegistry,IInvoiceService invoiceService, ILogger<InvoiceController> logger)
        {
            _metricsRegistry = metricsRegistry;
            _invoiceService = invoiceService;
            _logger = logger;
        }
        [HttpGet]
        public ActionResult GetAllInvoice()
        {
            _metricsRegistry.IncrementFindQuery();
            _logger.LogInformation("Get Invoice");
            var invoices = _invoiceService.GetAll().OrderBy(x=>x.IdInvoice);
            return Ok(invoices);
        }


        [HttpPost("Pay")]
        public IActionResult Pagos([FromBody] InvoiceRequest request)
        {

            var entityInvoice = _invoiceService.GetAll().Where(x => x.IdInvoice == request.IdInvoice).FirstOrDefault();

            if (entityInvoice.State.Equals("PAGADO"))
            {
                return BadRequest(new { message = $"La deuda ya se encuentra PAGADA." });
            }            

            if (entityInvoice.Amount != request.Amount)
            {
                var deuda = $",  =======> Deuda a Pagar: " + entityInvoice.Amount.ToString();
                return BadRequest(new { message = $"El Monto indicado es Incorrecto. " + deuda });
            }          
            _invoiceService.Pay(new Model.Invoice()
            {
                IdInvoice = request.IdInvoice,
                Amount = entityInvoice.Amount,
                State = "PAGADO"
            });

            return Ok();
        }
    }

}