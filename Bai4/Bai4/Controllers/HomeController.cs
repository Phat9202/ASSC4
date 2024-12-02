using Bai4.DbContextt;
using Bai4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
namespace Bai4.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyDb _context;
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger, MyDb context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.ListCustumers = _context.Custumers.ToList();
            return View();
        }
        public IActionResult ShowTempData()
        {
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
