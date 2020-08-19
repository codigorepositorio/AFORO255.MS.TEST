using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AFORO255.MS.TEST.Invoice.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AFORO255.MS.TEST.Invoice.Controllers
{
    [Route("api/Invoice")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }
        [HttpGet]
        public ActionResult GetAllInvoice()
        {
            var invoices = _invoiceService.GetAll();
            return Ok(invoices);
        }
    }
}
