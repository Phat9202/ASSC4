using Bai5.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using System.Reflection;

namespace Buoi5.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult CreateCustomer()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCustomer(Customer cus)
        {
            return View();
        }
        public IActionResult Index()
        {
            IEnumerable<Customer> list = new List<Customer>()
            {
                new Customer(1,"vlluon","Hà Nội"),
                new Customer(2,"vlluon1","Hà Ngoại")
            };
            return View(list);
        }
        public IActionResult Details(int id)
        {
            Customer cus = new Customer(1, "vlluon", "Hà Nội");

            return View(cus);
        }

    }
}