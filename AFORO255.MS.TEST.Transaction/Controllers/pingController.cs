using Microsoft.AspNetCore.Mvc;

namespace AFORO255.MS.TEST.Transaction.Controllers
{
    [Route("")]
    [ApiController]
    public class pingController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok();
        }
    }
}
