using AFORO255.MS.TEST.Pay.RabbitMQ.Commands;
using AFORO255.MS.TEST.Pay.RabbitMQ.Events;
using MediatR;
using MS.AFORO255.Cross.RabbitMQ.Src.Bus;
using System.Threading;
using System.Threading.Tasks;

namespace AFORO255.MS.TEST.Pay.RabbitMQ.CommandHandlers
{
    public class NotificationCommandHandler : IRequestHandler<NotificationCreateCommand,bool>
    {
        private readonly IEventBus _bus;

        public NotificationCommandHandler( IEventBus bus)
        {
            _bus = bus;
        }

        public Task<bool> Handle(NotificationCreateCommand request, CancellationToken cancellationToken)
        {
            _bus.Publish(new NotificationCreatedEvent(
                request.IdOperation,
                request.IdInvoice,
                request.Amount
                ));
            return Task.FromResult(true);
        }
    }
}
