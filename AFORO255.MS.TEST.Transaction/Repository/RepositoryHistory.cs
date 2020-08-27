using AFORO255.MS.TEST.Notification.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace AFORO255.MS.TEST.Notification.Repository
{
    public class RepositoryHistory: IRepositoryHistory
    {
        private readonly IMongoDatabase _database = null;

        public RepositoryHistory(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["mongo:cn"]);
            if (client != null)
                _database = client.GetDatabase(configuration["mongo:database"]);
        }
        public IMongoCollection<HistoryTransaction> HistoryPay
        {
            get
            {
                return _database.GetCollection<HistoryTransaction>("HistoryTransaction");
            }
        }
    }
}
