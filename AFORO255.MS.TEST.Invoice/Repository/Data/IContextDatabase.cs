using Microsoft.EntityFrameworkCore;

namespace AFORO255.MS.TEST.Invoice.Repository.Data
{
    public interface IContextDatabase
    {
       
            DbSet<AFORO255.MS.TEST.Invoice.Model.Invoice> Invoice { get; set; }
                
    }
}
