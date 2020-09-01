using MS.AFORO255.Cross.RabbitMQ.Src.Events;

namespace AFORO255.MS.TEST.Transaction.RabbitMQ.Events
{
    public class TransactionPayCreatedEvent : Event
    {
        public TransactionPayCreatedEvent(int idOperation, int idInvoice, decimal amount, string date)
        {
            IdOperation = idOperation;
            IdInvoice = idInvoice;
            Amount = amount;
            Date = date;
        }

        public int IdOperation { get; protected set; }
        public int IdInvoice { get; protected set; }
        public decimal Amount { get; protected set; }
        public string Date { get; protected set; }
    }
}

