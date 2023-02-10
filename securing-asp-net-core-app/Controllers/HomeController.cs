using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using securing_asp_net_core_app.Models;

namespace securing_asp_net_core_app.Controllers
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult DemoPost()
        {
            return View();
        }

        //NOTE: 1A
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DemoPost(string data)
        {
            ViewData["data"] = data;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}