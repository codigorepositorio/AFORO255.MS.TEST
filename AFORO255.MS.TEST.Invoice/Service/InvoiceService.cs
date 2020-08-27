using AFORO255.MS.TEST.Invoice.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AFORO255.MS.TEST.Invoice.Service
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository )
        {
            _invoiceRepository = invoiceRepository;
        }

        public IEnumerable<Model.Invoice> GetAll()
        {
            return _invoiceRepository.GetAll();
        }

        public bool Pay(Model.Invoice invoice)
        {
            return _invoiceRepository.Pay(invoice);
        }
    }
}
