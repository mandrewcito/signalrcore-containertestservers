namespace SignalRSample.Controllers
{
	using System.Diagnostics;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;
	using Models;

	public class HealthController : Controller
    {
        private readonly ILogger<HealthController> _logger;

        public HealthController(ILogger<HealthController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return Ok(new {
             status="ok"   
            });
        }
    }
}
