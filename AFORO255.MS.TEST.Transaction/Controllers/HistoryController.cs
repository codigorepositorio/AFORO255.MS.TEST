using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AFORO255.MS.TEST.Transaction.DTO;
using AFORO255.MS.TEST.Transaction.Model;
using AFORO255.MS.TEST.Transaction.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using MS.AFORO255.Cross.Metrics.Registry;

namespace AFORO255.MS.TEST.Transaction.Controllers
{
    [Route("api/History")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IMetricsRegistry _metricsRegistry;
        private readonly IHistoryService _historyService;
        private readonly ILogger<HistoryController> _logger;

        public HistoryController(IDistributedCache distributedCache, IMetricsRegistry metricsRegistry, IHistoryService historyService, ILogger<HistoryController> logger)
        {
            _distributedCache = distributedCache;
            _metricsRegistry = metricsRegistry;
            _historyService = historyService;
            _logger = logger;
        }

        [HttpGet("{idinvoice}")]
        public async Task<IActionResult> GetHistory(int idinvoice)
        {
            _metricsRegistry.IncrementFindQuery();
            _logger.LogInformation("Get History {idinvoice} ");
            string _key = $"key-invoice-{idinvoice}";

            var _cache = _distributedCache.GetString(_key);
            List<HistoryResponse> model = null;
            if (_cache == null)
            {
                var result = await _historyService.GetAll();
                model = result.Where(x => x.Idinvoice.Equals(idinvoice)).ToList();

                var options = new DistributedCacheEntryOptions()
                                    .SetSlidingExpiration(TimeSpan.FromSeconds(30));
                _distributedCache.SetString(_key, Newtonsoft.Json.JsonConvert.SerializeObject(model), options);
            }
            else
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<List<HistoryResponse>>(_cache);
            }
            return Ok(model);

       }

        [HttpPost]
        public async Task<IActionResult> CreateHistory(HistoryTransaction request)
        {
            await _historyService.Add(request);
            return Ok();
        }
    }
}
