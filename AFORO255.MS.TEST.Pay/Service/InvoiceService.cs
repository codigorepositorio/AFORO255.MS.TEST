using AFORO255.MS.TEST.Pay.DTO;
using AFORO255.MS.TEST.Pay.Model;
using Microsoft.Extensions.Configuration;
using MS.AFORO255.Cross.Proxy.Proxy;
using Polly;
using Polly.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AFORO255.MS.TEST.Pay.Service
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ITransactionService _transactionService;
        private readonly IConfiguration _configuration;
        private readonly IHttpClient _httpClient;

        public InvoiceService(ITransactionService transactionService,
            IConfiguration configuration, IHttpClient httpClient)
        {
            _transactionService = transactionService;
            _configuration = configuration;
            _httpClient = httpClient;
        }
        public IEnumerable<Transaction> GetAll()
        {
            return _transactionService.GetAll();
        }
        public async Task<bool> PayInvoice(InvoiceRequest request)
        {
            string uri = _configuration["proxy:urlInvoicePago"];
            var response = await _httpClient.PostAsync(uri, request);
            response.EnsureSuccessStatusCode();
            return true;

        }

        public bool ReverseInvoice(Transaction request)
        {
            _transactionService.PayReverse(request);
            return true;
        }

        public bool Execute(Transaction request)
        {

            bool response = false;
            //#1. Pòlitica de CircuiBraker

            var circuitBrakerPolicy = Policy.Handle<Exception>()
                .CircuitBreaker(3, TimeSpan.FromSeconds(15),
                onBreak: (ex, timeSpan, context) =>
                {
                    Console.Write("El circuito entro en estado de falla");
                },
                 onReset: (context) =>
                 {
                     Console.Write("Circuito dejo estado de falla");
                 });

            //#2 Pòlitica de reitentos
            var retry = Policy.Handle<Exception>()
                .WaitAndRetryForever(attemp => TimeSpan.FromMilliseconds(300))
                .Wrap(circuitBrakerPolicy);//Se antepone esta Politica de CircuiBraker.

            //#3 La ejecuciòn de Reitentos.
            retry.Execute(() =>
            {
                if (circuitBrakerPolicy.CircuitState == CircuitState.Closed)
                {
                    circuitBrakerPolicy.Execute(() =>
                    {
                        InvoiceRequest invoice = new InvoiceRequest()
                        {
                            IdInvoice = request.IdInvoice,
                            Amount = request.Amount                           
                        };
                        response = PayInvoice(invoice).Result;
                        Console.WriteLine("Solicitud realizada con èxito");
                    });

                }//fin del IF


                if (circuitBrakerPolicy.CircuitState != CircuitState.Closed)
                {

                    Transaction transaction = new Transaction()
                    {
                        IdInvoice = request.IdInvoice,
                        Amount = request.Amount,
                        Date = DateTime.Now.ToString() + $"  CANCELADO"
                    };

                    response = ReverseInvoice(transaction);
                    response = false;
                    Console.WriteLine("Solicitud cancelada con èxito");

                }//fin del IF

            });//Fin Retry

            return response;
        }


    }
}
