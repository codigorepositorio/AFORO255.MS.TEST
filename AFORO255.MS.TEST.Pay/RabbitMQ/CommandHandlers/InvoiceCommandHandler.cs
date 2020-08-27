using AFORO255.MS.TEST.Pay.RabbitMQ.Commands;
using AFORO255.MS.TEST.Pay.RabbitMQ.Events;
using MediatR;
using MS.AFORO255.Cross.RabbitMQ.Src.Bus;
using System.Threading;
using System.Threading.Tasks;

namespace AFORO255.MS.TEST.Pay.RabbitMQ.CommandHandlers
{
    public class InvoiceCommandHandler : IRequestHandler<InvoiceCreatedCommand, bool>
    {
        private readonly IEventBus _bus;

        public InvoiceCommandHandler(IEventBus bus)
        {
            _bus = bus;
        }
        public Task<bool> Handle(InvoiceCreatedCommand request, CancellationToken cancellationToken)
        {
            _bus.Publish(new InvoiceCreatedEvent(
               request.IdInvoice,               
               request.Amount,
               request.State
                   ));
            return Task.FromResult(true);
        }
    }
}
