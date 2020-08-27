using MS.AFORO255.Cross.RabbitMQ.Src.Commands;

namespace AFORO255.MS.TEST.Pay.RabbitMQ.Commands
{
    public class NotificationCreateCommand: Command
    {

        public NotificationCreateCommand(int idOperation, int idInvoice, decimal amount)
        {
            IdOperation = idOperation;
            IdInvoice = idInvoice;
            Amount = amount;            
        }

        public int IdOperation { get; protected set; }
        public int IdInvoice { get; protected set; }
        public decimal Amount { get; protected set; }
        
    }
}
