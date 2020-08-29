namespace AFORO255.MS.TEST.Pay.RabbitMQ.Commands
{
    public class TransactionPayCreatedCommand : TransactionPayCommand
    {
        public TransactionPayCreatedCommand(int idOperation, int idInvoice, decimal amount, string date)
        {
            IdOperation = idOperation;
            IdInvoice = idInvoice;
            Amount = amount;
            Date = date;
        }
    }
}
