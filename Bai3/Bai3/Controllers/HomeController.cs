using Bai3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bai3.Controllers
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
            Custumer custumer1 = new Custumer()
            {
                id = 1,
                Name = "Phat",
                Adresss = "Hà Nội"
            };
            Custumer custumer2 = new Custumer()
            {
                id = 2,
                Name = "Nam",
                Adresss = "Sài Gòn"
            };
            ListCustumer listCustumer = new ListCustumer() { };
            listCustumer.listCustumer.Add(custumer1);
            listCustumer.listCustumer.Add(custumer2);
            ;
            return View(listCustumer);
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
