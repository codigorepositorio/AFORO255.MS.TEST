using AFORO255.MS.TEST.Pay.Model;
using Microsoft.EntityFrameworkCore;

namespace AFORO255.MS.TEST.Pay.Repository.Data
{
    public class ContextDataBase : DbContext, IContextDataBase
    {
        public ContextDataBase(DbContextOptions<ContextDataBase> options):base(options)
        {

        }        
        public DbSet<Transaction> Transaction { get; set; }
    }

}
