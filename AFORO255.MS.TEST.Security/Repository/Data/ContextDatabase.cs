using Microsoft.EntityFrameworkCore;
using AFORO255.MS.TEST.Security.Model;

namespace AFORO255.MS.TEST.Security.Repository.Data
{
    public class ContextDatabase : DbContext, IContextDatabase
    {
        public ContextDatabase( DbContextOptions<ContextDatabase> options) : base(options)
        {
        }
        public DbSet<Access> Access { get; set; }
    }
}
