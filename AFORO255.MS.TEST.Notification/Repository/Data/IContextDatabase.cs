

using AFORO255.MS.TEST.Notification.Model;
using Microsoft.EntityFrameworkCore;

namespace AFORO255.MS.TEST.Notification.Repository.Data
{
    public interface IContextDatabase
    {
        DbSet<SendMail> SendMail { get; set; }
        int SaveChanges();
    }
}
