using AFORO255.MS.TEST.Pay.Model;
using Microsoft.EntityFrameworkCore;

namespace AFORO255.MS.TEST.Pay.Repository.Data
{
    public interface IContextDataBase
    {
        DbSet<Transaction> Transaction { get; set; }
        int SaveChanges();
    }
}
