using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AFORO255.MS.TEST.Pay.Model
{
    [Table("Pay")]
    public class Transaction
    {
        [Key]
        [Column("id_operation")]
        public int IdOperation { get; set; }


        [Column("id_invoice")]
        public int IdInvoice { get; set; }
        public decimal Amount { get; set; }
        public string Date { get; set; }
    }
}
