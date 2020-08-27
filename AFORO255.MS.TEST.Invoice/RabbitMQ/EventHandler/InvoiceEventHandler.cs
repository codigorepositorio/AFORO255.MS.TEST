
using AFORO255.MS.TEST.Invoice.Model;
using AFORO255.MS.TEST.Invoice.RabbitMQ.Events;
using AFORO255.MS.TEST.Invoice.Service;
using MS.AFORO255.Cross.RabbitMQ.Src.Bus;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AFORO255.MS.TEST.Invoice.RabbitMQ.EventHandler
{
    public class InvoiceEventHandler : IEventHandler<InvoiceCreatedEvent>
    {
        private readonly IInvoiceService _transactionService;

        public InvoiceEventHandler(IInvoiceService transactionService)
        {
            _transactionService = transactionService;
        }
        public Task Handle(InvoiceCreatedEvent @event)
        {

            //var result = _transactionService.GetAll().Where(x=>x.IdInvoice == @event.IdInvoice).FirstOrDefault();

            _transactionService.Pay(new Model.Invoice()
            {
                IdInvoice = @event.IdInvoice,             
                Amount = @event.Amount,
                State = "PAGADO"
            });
            return Task.CompletedTask;
        }
    }
}
