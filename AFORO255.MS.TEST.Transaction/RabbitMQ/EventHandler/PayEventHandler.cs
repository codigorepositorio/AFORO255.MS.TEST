
using AFORO255.MS.TEST.Notification.Model;
using AFORO255.MS.TEST.Notification.RabbitMQ.Events;
using AFORO255.MS.TEST.Notification.Service;
using MS.AFORO255.Cross.RabbitMQ.Src.Bus;
using System.Threading.Tasks;

namespace AFORO255.MS.TEST.Notification.RabbitMQ.EventHandler
{
    public class PayEventHandler : IEventHandler<PayCreatedEvent>
    {
        private readonly IHistoryService _transactionService;

        public PayEventHandler(IHistoryService transactionService)
        {
            _transactionService = transactionService;
        }
        public Task Handle(PayCreatedEvent @event)
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
