using AFORO255.MS.TEST.Pay.Model;
using System.Collections.Generic;

namespace AFORO255.MS.TEST.Pay.Repository
{
    public interface ITransactionRepository
    {
        Transaction Pay(Transaction transaction);
        Transaction PayReverse(Transaction transaction);
        IEnumerable<Transaction> GetAll();


    }
}
