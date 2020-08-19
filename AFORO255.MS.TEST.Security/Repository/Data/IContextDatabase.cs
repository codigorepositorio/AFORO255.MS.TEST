using Microsoft.EntityFrameworkCore;
using AFORO255.MS.TEST.Security.Model;

namespace AFORO255.MS.TEST.Security.Repository.Data
{
    public interface IContextDatabase
    {
        DbSet<Access> Access { get; set; }
    }
}
