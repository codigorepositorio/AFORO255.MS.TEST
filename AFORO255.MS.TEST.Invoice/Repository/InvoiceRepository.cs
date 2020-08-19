using AFORO255.MS.TEST.Invoice.Repository.Data;
using System.Collections.Generic;
using System.Linq;

namespace AFORO255.MS.TEST.Invoice.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly IContextDatabase _contextDatabase;

        public InvoiceRepository(IContextDatabase contextDatabase)
        {
            _contextDatabase = contextDatabase;
        }
        public IEnumerable<Model.Invoice> GetAll()
        {
            return _contextDatabase.Invoice.ToList();
        }
    }
}
