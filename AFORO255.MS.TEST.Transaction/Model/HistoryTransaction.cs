using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AFORO255.MS.TEST.Transaction.Model
{

    public class HistoryTransaction
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public int Idtransaccion { get; set; }
        public int IdInvoice { get; set; }
        public decimal Amount { get; set; }
        public string Date { get; set; }

    }
}
