using MS.AFORO255.Cross.RabbitMQ.Src.Events;

namespace AFORO255.MS.TEST.Pay.RabbitMQ.Events
{
    public class NotificationCreatedEvent:Event
    {
        public NotificationCreatedEvent(int idOperation, int idInvoice, decimal amount)
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
