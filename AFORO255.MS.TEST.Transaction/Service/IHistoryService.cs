using AFORO255.MS.TEST.Notification.DTO;
using AFORO255.MS.TEST.Notification.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AFORO255.MS.TEST.Notification.Service
{
    public interface IHistoryService
    {
        Task<IEnumerable<HistoryResponse>> GetAll();
        Task<bool> Add(HistoryTransaction historyTransaction);
    }
}
