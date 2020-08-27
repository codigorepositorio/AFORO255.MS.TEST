using MS.AFORO255.Cross.RabbitMQ.Src.Commands;
namespace AFORO255.MS.TEST.Pay.RabbitMQ.Commands
{
    public class InvoiceCommand : Command
    {
        public int IdInvoice { get; protected set; }
        public decimal Amount { get; protected set; }
        public string State { get; protected set; }
    }
}
