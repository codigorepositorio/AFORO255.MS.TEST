using System.Collections.Generic;
using System.Threading.Tasks;

namespace AFORO255.MS.TEST.Invoice.Service
{
    public interface IInvoiceService
    {          
        IEnumerable<Model.Invoice> GetAll();
        bool Pay(Model.Invoice invoice);  //Hacer un pago.
    }
}
