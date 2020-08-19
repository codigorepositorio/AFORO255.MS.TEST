using AFORO255.MS.TEST.Pay.Model;
using AFORO255.MS.TEST.Pay.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AFORO255.MS.TEST.Pay.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IContextDataBase _contextDataBase;

        public TransactionRepository(IContextDataBase contextDataBase)
        {
            _contextDataBase = contextDataBase;
        }
        public IEnumerable<Transaction> GetAll()
        {
            return _contextDataBase.Transaction.ToList();
        }

        public Transaction Pay(Transaction transaction)
        {
             _contextDataBase.Transaction.Add(transaction);
            _contextDataBase.SaveChanges();
            return transaction;
        }
    }
}
