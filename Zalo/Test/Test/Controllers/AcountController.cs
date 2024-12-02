using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    public class AcountCOntroller : Controller
    {
        private readonly AuthenticationService _authenticationService;

        public AcountCOntroller(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string UserName, string PassWord)
        {
            if (UserName == "admin" && PassWord == "123")
            {
                HttpContext.Session.GetString("UserName");
                HttpContext.Session.GetString("PassWord");
                return RedirectToAction("/Home/Index");
            }
            return View();
        }
    }
}
