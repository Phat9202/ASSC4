using Microsoft.AspNetCore.Mvc;
using Test3.Context;
using Test3.Models;

namespace Test3.Controllers
{
    public class LoginController : Controller
    {
        private readonly MyDb _context;

        public LoginController(MyDb context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Index(Login login)
        {
            var check=_context.Logins.FirstOrDefault(x=>x.TaiKhoan == login.TaiKhoan);
            if(login.TaiKhoan==check.TaiKhoan && login.MatKhau == check.MatKhau)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(login);
        }
    }
}
