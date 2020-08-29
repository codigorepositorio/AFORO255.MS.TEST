using System.Linq;
using AFORO255.MS.TEST.Notification.Model;
using AFORO255.MS.TEST.Notification.Repository;
using AFORO255.MS.TEST.Notificationn.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AFORO255.MS.TEST.Notification.Controllers
{
    [Route("api/Notification")]
    [ApiController]
    public class NotificationController : Controller
    {
        private readonly IMailRepository _mailRepository;

        public NotificationController(IMailRepository mailRepository)
        {
            _mailRepository = mailRepository;
        }

        [HttpPost]
        public IActionResult CreateNotification ([FromBody] SendMailDto request)
        {
            SendMail mail = new SendMail()
            {
                SendDate = request.SendDate,
                AccountId = request.AccountId
            };
            _mailRepository.Add(mail);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetNotification(int id)
        {
            var entity = _mailRepository.GetAll();            
            return Ok(entity);
        }
    }
}
