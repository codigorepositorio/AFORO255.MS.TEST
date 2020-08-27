using AFORO255.MS.TEST.Pay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AFORO255.MS.TEST.Pay.Service
{
    public interface ITransactionService
    {
        Transaction Pay(Transaction transaction);
        Transaction PayReverse(Transaction transaction);
        IEnumerable<Transaction> GetAll();
    }
}
