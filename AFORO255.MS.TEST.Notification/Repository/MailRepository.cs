using AFORO255.MS.TEST.Notification.Model;
using AFORO255.MS.TEST.Notification.Repository.Data;
using System.Collections.Generic;
using System.Linq;

namespace AFORO255.MS.TEST.Notification.Repository
{
    public class MailRepository : IMailRepository
    {
        private readonly IContextDatabase _contextDatabase;

        public MailRepository(IContextDatabase contextDatabase)
        {
            _contextDatabase = contextDatabase;
        }

        public bool Add(SendMail sendMail)
        {
            _contextDatabase.SendMail.Add(sendMail);
            _contextDatabase.SaveChanges();
            return true;
        }

        public IEnumerable<SendMail> GetAll()
        {
         return _contextDatabase.SendMail.ToList();


        }
    }
}
