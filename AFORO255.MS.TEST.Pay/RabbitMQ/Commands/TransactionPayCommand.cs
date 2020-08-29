using MS.AFORO255.Cross.RabbitMQ.Src.Commands;
namespace AFORO255.MS.TEST.Pay.RabbitMQ.Commands
{
    public class TransactionPayCommand : Command
    {        
        public int IdOperation { get; protected set; }
        public int IdInvoice { get; protected set; }
        public decimal Amount { get; protected set; }
        public string Date { get; protected set; }
    }
}
