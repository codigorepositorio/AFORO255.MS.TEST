using Microsoft.EntityFrameworkCore;

namespace AFORO255.MS.TEST.Invoice.Repository.Data
{
    public interface IContextDatabase
    {
       
            DbSet<Model.Invoice> Invoice { get; set; }
             int SaveChanges();

    }
}
