using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AFORO255.MS.TEST.Invoice.DTO
{
    
    public class InvoiceRequest
    {
        
        public int IdInvoice { get; set; }
        public decimal Amount { get; set; }     
        public string State { get; set; }
    }
}
