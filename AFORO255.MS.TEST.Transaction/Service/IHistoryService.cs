using AFORO255.MS.TEST.Transaction.DTO;
using AFORO255.MS.TEST.Transaction.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AFORO255.MS.TEST.Transaction.Service
{
    public interface IHistoryService
    {
        Task<IEnumerable<HistoryResponse>> GetAll();
        Task<bool> Add(HistoryTransaction historyTransaction);
    }
}
