
using AFORO255.MS.TEST.Transaction.Model;
using AFORO255.MS.TEST.Transaction.RabbitMQ.Events;
using AFORO255.MS.TEST.Transaction.Service;
using MS.AFORO255.Cross.RabbitMQ.Src.Bus;
using System.Threading.Tasks;

namespace AFORO255.MS.TEST.Transaction.RabbitMQ.EventHandler
{
    public class TransactionPayEventHandler : IEventHandler<TransactionPayCreatedEvent>
    {
        private readonly IHistoryService _transactionService;

        public TransactionPayEventHandler(IHistoryService transactionService)
        {
            _transactionService = transactionService;
        }
        public Task Handle(TransactionPayCreatedEvent @event)
        {
            _transactionService.Add(new HistoryTransaction()
            {
                Idtransaccion = @event.IdOperation,
                IdInvoice = @event.IdInvoice,
                Amount = @event.Amount,
                Date = @event.Date
            });
            return Task.CompletedTask;
        }
    }
}
