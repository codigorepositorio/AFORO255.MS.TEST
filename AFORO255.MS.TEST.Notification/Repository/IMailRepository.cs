using AFORO255.MS.TEST.Notification.Model;
using System.Collections;
using System.Collections.Generic;

namespace AFORO255.MS.TEST.Notification.Repository
{
    public interface IMailRepository
    {
        bool Add(SendMail sendMail);
        IEnumerable<SendMail> GetAll();
    }
}
