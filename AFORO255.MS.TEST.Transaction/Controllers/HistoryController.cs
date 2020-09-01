using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AFORO255.MS.TEST.Transaction.Model;
using AFORO255.MS.TEST.Transaction.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AFORO255.MS.TEST.Transaction.Controllers
{
    [Route("api/History")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryService _historyService;

        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet("{Idinvoice}")]
        public async Task<IActionResult> GetHistory(int Idinvoice)
        {
            var result = await _historyService.GetAll();

            //var data = result.Where(x => x.Idinvoice.Equals(Idinvoice)).ToList();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHistory(HistoryTransaction request)
        {
            await _historyService.Add(request);
            return Ok();
        }
    }
}
