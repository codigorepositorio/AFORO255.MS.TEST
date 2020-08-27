using AFORO255.MS.TEST.Invoice.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AFORO255.MS.TEST.Invoice.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly IContextDatabase _contextDatabase;

        public InvoiceRepository(IContextDatabase contextDatabase)
        {
            _contextDatabase = contextDatabase;
        }

        public bool Pay(Model.Invoice invoice)
        {
            _contextDatabase.Invoice.Update(invoice);
            _contextDatabase.SaveChanges();
            return true;
        }

        public IEnumerable<Model.Invoice> GetAll()
        {
            return _contextDatabase.Invoice.AsNoTracking().ToList();
        }

    }
}
