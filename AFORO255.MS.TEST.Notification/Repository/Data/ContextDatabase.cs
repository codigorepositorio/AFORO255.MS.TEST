

using AFORO255.MS.TEST.Notification.Model;
using AFORO255.MS.TEST.Notification.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace AFORO255.MS.TEST.Notification
{
    public class ContextDatabase : DbContext,IContextDatabase
    {
        public ContextDatabase( DbContextOptions<ContextDatabase> options) : base(options)
        {

        }

        public DbSet<SendMail> SendMail { get; set; }
        public DbContext Instance => this;


    }
}
