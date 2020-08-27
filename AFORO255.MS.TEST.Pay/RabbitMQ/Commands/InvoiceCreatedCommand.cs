namespace AFORO255.MS.TEST.Pay.RabbitMQ.Commands
{
    public class InvoiceCreatedCommand : InvoiceCommand
    {
        public InvoiceCreatedCommand(int idInvoice, decimal amount, string state)
        {

            IdInvoice = idInvoice;
            Amount = amount;
            State = state;
        }
        
    }
}

