using System.Linq;
using AFORO255.MS.TEST.Notification.Model;
using AFORO255.MS.TEST.Notification.Repository;
using AFORO255.MS.TEST.Notificationn.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MS.AFORO255.Cross.Metrics.Registry;

namespace AFORO255.MS.TEST.Notification.Controllers
{
    [Route("api/Notification")]
    [ApiController]
    public class NotificationController : Controller
    {
        private readonly IMetricsRegistry _metricsRegistry;
        private readonly IMailRepository _mailRepository;
        private readonly ILogger<NotificationController> _logger;

        public NotificationController(IMetricsRegistry metricsRegistry,IMailRepository mailRepository, ILogger<NotificationController> logger)
        {
            _metricsRegistry = metricsRegistry;
            _mailRepository = mailRepository;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult CreateNotification ([FromBody] SendMailDto request)
        {

            SendMail mail = new SendMail()
            {
                SendDate = request.SendDate,
                InvoiceId = request.InvoiceId
            };
            _mailRepository.Add(mail);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetNotification(int id)
        {
            _logger.LogInformation("Get Notification");
            _metricsRegistry.IncrementFindQuery();
            var entity = _mailRepository.GetAll();            
            return Ok(entity);
        }
    }
}
