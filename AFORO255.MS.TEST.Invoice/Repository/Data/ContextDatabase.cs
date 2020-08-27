using Microsoft.EntityFrameworkCore;

namespace AFORO255.MS.TEST.Invoice.Repository.Data
{
    public class ContextDatabase : DbContext, IContextDatabase
    {
        public ContextDatabase(DbContextOptions<ContextDatabase> options) : base(options)
        {

        }
        public DbSet<Model.Invoice> Invoice { get; set; }
    }
}
