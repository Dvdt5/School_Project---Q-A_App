using Microsoft.AspNetCore.Mvc;

namespace School_Prohect___Q_A_App.UI.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public AdminController(ILogger<HomeController> logger, IConfiguration configuration)
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

        [Route("User")]
        public IActionResult Users(int id)
        {
            var ApiBaseURL = _configuration["ApiBaseURL"];
            ViewBag.ApiBaseURL = ApiBaseURL;
            return View();
        }

        [Route("Profile")]
        public IActionResult Profile(int id)
        {
            var ApiBaseURL = _configuration["ApiBaseURL"];
            ViewBag.ApiBaseURL = ApiBaseURL;
            return View();
        }

        [Route("Category")]
        public IActionResult Categories(int id)
        {
            var ApiBaseURL = _configuration["ApiBaseURL"];
            ViewBag.ApiBaseURL = ApiBaseURL;
            return View();
        }

        [Route("Posts")]
        public IActionResult Posts(int id)
        {
            var ApiBaseURL = _configuration["ApiBaseURL"];
            ViewBag.ApiBaseURL = ApiBaseURL;
            return View();
        }

        [Route("Comments")]
        public IActionResult Comments(int id)
        {
            var ApiBaseURL = _configuration["ApiBaseURL"];
            ViewBag.ApiBaseURL = ApiBaseURL;
            return View();
        }

        [Route("Login")]
        public IActionResult Login()
        {
            var ApiBaseURL = _configuration["ApiBaseURL"];
            ViewBag.ApiBaseURL = ApiBaseURL;
            return View();
        }
    }
}
