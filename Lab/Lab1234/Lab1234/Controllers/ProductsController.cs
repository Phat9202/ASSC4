using Lab1234.Data;
using Lab1234.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab1234.Controllers
{
    public class ProductsController : Controller
    {
        public MyDB MyDB;

        public ProductsController(MyDB myDB)
        {
            MyDB = myDB;
        }

        public IActionResult Search(string NameSearch, string CategorySearch)
        {
            var product = MyDB.Products.Where(x => x.Name == NameSearch && x.Category == CategorySearch);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        public IActionResult Index()
        {
            return View(MyDB.Products);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid) 
            {
                MyDB.Products.Add(product);
                MyDB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }
    }
}
