using Assignment2.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Assignment2.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogTrace("Trace Log Message");
            _logger.LogDebug("Debug Log Message");
            _logger.LogInformation("Information Log Message");
            _logger.LogWarning("Warning Log Message");
            _logger.LogError("Error Log Message");
            _logger.LogCritical("Fatal Log Message");
            return View();
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("I am in Privacy");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}