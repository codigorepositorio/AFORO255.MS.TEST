using AFORO255.MS.TEST.Security.DTO;
using AFORO255.MS.TEST.Security.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MS.AFORO255.Cross.Jwt.Src;
using MS.AFORO255.Cross.Metrics.Registry;

namespace AFORO255.MS.TEST.Security.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccessService _accessService;
        private readonly IMetricsRegistry _metricsRegistry;
        private readonly ILogger<AuthController> _logger;
        private readonly JwtOptions _options;

        public AuthController(IAccessService accessService, ILogger<AuthController> logger,IOptionsSnapshot<JwtOptions> options, IMetricsRegistry metricsRegistry)
        {
       
           _metricsRegistry = metricsRegistry;
            _accessService = accessService;
            _logger = logger;
            _options = options.Value;
        }
        [HttpGet]
        public IActionResult GetAllUsuarios()
        {
            _metricsRegistry.IncrementFindQuery();
            _logger.LogInformation("Get Security(Auth)");
            var usuarios = _accessService.GetAll();
            return Ok(usuarios);
        }

        [HttpPost]
        public IActionResult Login( [FromBody] AuthRequest request)
        {
            _metricsRegistry.IncrementFindQuery();
            _logger.LogInformation("Post Security(Auth)");
            var acceso = _accessService.Validate(request.UserName,request.Password);
            if (!acceso)
            {
                return Unauthorized();
            }            
            Response.Headers.Add("access-control-expose-headers","Authorization");
            Response.Headers.Add("Authorization",JwtToken.Create(_options));
            return Ok();
        }
    }
}
