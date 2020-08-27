using MS.AFORO255.Cross.RabbitMQ.Src.Events;

namespace AFORO255.MS.TEST.Pay.RabbitMQ.Events
{
    public class InvoiceCreatedEvent : Event
    {
        public InvoiceCreatedEvent(int idInvoice, decimal amount, string state)
        {

            IdInvoice = idInvoice;
            Amount = amount;
            State = state;
        }
        public int IdInvoice { get; protected set; }
        public decimal Amount { get; protected set; }
        public string State { get; protected set; }
    }
}
