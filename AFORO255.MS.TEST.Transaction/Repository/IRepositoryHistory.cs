using AFORO255.MS.TEST.Notification.Model;
using MongoDB.Driver;

namespace AFORO255.MS.TEST.Notification.Repository
{
    public interface IRepositoryHistory
    {
        IMongoCollection<HistoryTransaction> HistoryPay { get; } 
    }
}
