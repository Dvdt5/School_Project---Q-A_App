using Microsoft.AspNetCore.Mvc;
using School_Prohect___Q_A_App.UI.Models;
using System.Diagnostics;

namespace School_Prohect___Q_A_App.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var ApiBaseURL = _configuration["ApiBaseURL"];
            ViewBag.ApiBaseURL = ApiBaseURL;
            return View();
        }

        public IActionResult SinglePost(int id)
        {
            var ApiBaseURL = _configuration["ApiBaseURL"];
            ViewBag.ApiBaseURL = ApiBaseURL;
            ViewBag.SinglePostId = id;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
