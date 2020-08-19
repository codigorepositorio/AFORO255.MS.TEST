using AFORO255.MS.TEST.Pay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AFORO255.MS.TEST.Pay.Repository
{
    public interface ITransactionRepository
    {
        Transaction Pay(Transaction transaction);
        IEnumerable<Transaction> GetAll();


    }
}
