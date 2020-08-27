using AFORO255.MS.TEST.Pay.DTO;
using AFORO255.MS.TEST.Pay.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AFORO255.MS.TEST.Pay.Service
{
    public interface IInvoiceService
    {
        IEnumerable<Transaction> GetAll();
        Task<bool> PayInvoice(InvoiceRequest request);
        bool ReverseInvoice(Transaction request);
        bool Execute(Transaction request);
    }
}
