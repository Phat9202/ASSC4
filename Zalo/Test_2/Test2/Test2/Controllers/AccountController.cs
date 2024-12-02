using Microsoft.AspNetCore.Mvc;

namespace Test2.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string UserName,string PassWord)
        {
            if(UserName=="admin"&& PassWord == "123")
            {
                HttpContext.Session.GetString("UserName");
                HttpContext.Session.GetString("PassWord");
                return RedirectToAction("/Home/Index");
            }
            return View();
        }
    }
}
