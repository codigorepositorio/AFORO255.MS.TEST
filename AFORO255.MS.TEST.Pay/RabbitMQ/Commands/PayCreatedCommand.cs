namespace AFORO255.MS.TEST.Pay.RabbitMQ.Commands
{
    public class PayCreatedCommand : PayCommand
    {
        public PayCreatedCommand(int idOperation, int idInvoice, decimal amount, string date)
        {
            IdOperation = idOperation;
            IdInvoice = idInvoice;
            Amount = amount;
            Date = date;
        }
    }
}
