using AFORO255.MS.TEST.Transaction.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace AFORO255.MS.TEST.Transaction.Repository
{
    public class RepositoryHistory: IRepositoryHistory
    {
        private readonly IMongoDatabase _database = null;

        public RepositoryHistory(IConfiguration configuration)
        {
            //var client = new MongoClient(configuration["mongo:cn"]);
            var client = new MongoClient(configuration["cnmongo"]);

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
