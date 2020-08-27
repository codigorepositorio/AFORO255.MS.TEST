
using AFORO255.MS.TEST.Notification.Model;
using AFORO255.MS.TEST.Notification.RabbitMQ.Events;
using AFORO255.MS.TEST.Notification.Repository;
using MS.AFORO255.Cross.RabbitMQ.Src.Bus;
using System;
using System.Threading.Tasks;

namespace AFORO255.MS.TEST.Notification.RabbitMQ.EventHandler
{
    public class NotificationEventHandler : IEventHandler<NotificationCreatedEvent>
    {
        private readonly IMailRepository _iMailRepository;

        public NotificationEventHandler(IMailRepository iMailRepository)
        {
            _iMailRepository = iMailRepository;
        }
        public Task Handle(NotificationCreatedEvent @event)
        {
            _iMailRepository.Add(new SendMail()
            {
                SendMailId = @event.IdOperation,
                AccountId = @event.IdInvoice,                
                SendDate = DateTime.Now.ToShortDateString()
            });
            return Task.CompletedTask;
        }
    }
}
