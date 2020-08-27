using AFORO255.MS.TEST.Pay.Model;
using AFORO255.MS.TEST.Pay.Repository;
using System.Collections.Generic;

namespace AFORO255.MS.TEST.Pay.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository )
        {
            _transactionRepository = transactionRepository;
        }
        public IEnumerable<Transaction> GetAll()
        {
            return _transactionRepository.GetAll();
        }

        public Transaction PayReverse(Transaction transaction)
        {
            _transactionRepository.PayReverse(transaction);
            return transaction;
        }

        public Transaction Pay(Transaction transaction)
        {
            _transactionRepository.Pay(transaction);
            return transaction;
        }
    }
}
