using AFORO255.MS.TEST.Notification.DTO;
using AFORO255.MS.TEST.Notification.Model;
using AFORO255.MS.TEST.Notification.Repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AFORO255.MS.TEST.Notification.Service
{
    public class HistoryService : IHistoryService
    {
        private readonly IRepositoryHistory _repositoryHistory;

        public HistoryService(IRepositoryHistory repositoryHistory)
        {
            _repositoryHistory = repositoryHistory;
        }

        public async Task<bool> Add(HistoryTransaction historyTransaction)
        {
            await _repositoryHistory.HistoryPay.InsertOneAsync(historyTransaction);
            return true;
        }

        public async Task<IEnumerable<HistoryResponse>> GetAll()
        {

            var data = await _repositoryHistory.HistoryPay.Find(_ => true).ToListAsync();
            var response = new List<HistoryResponse>();

            foreach (var item in data)
            {
                response.Add(new HistoryResponse()
                {
                    Idtransaccion = item.Idtransaccion,
                    Idinvoice=item.IdInvoice,
                    Amount=item.Amount,
                    Date=item.Date
                }) ;
            }
            return response;
        }

    }
}
