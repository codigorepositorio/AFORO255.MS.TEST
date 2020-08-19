using Microsoft.EntityFrameworkCore;

namespace AFORO255.MS.TEST.Invoice.Repository.Data
{
    public class ContextDatabase : DbContext, IContextDatabase
    {
        public ContextDatabase(DbContextOptions<ContextDatabase> options) : base(options)
        {

        }
        public DbSet<AFORO255.MS.TEST.Invoice.Model.Invoice> Invoice { get; set; }

        //public ContextDatabase(DbContextOptions<ContextDatabase> options) : base(options)
        //{
        //}
        //public DbSet<Access> Access
        //{
        //    get; set;
        }
}
