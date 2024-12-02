using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASS.Data;
using ASS.Models;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.IdentityModel.Tokens;

namespace ASS.Controllers
{
    public class AccountsController : Controller
    {
        private readonly MyDb _context;

        public AccountsController(MyDb context)
        {
            _context = context;
        }

        // GET: Accounts
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var account = await _context.Accounts
                    .FirstOrDefaultAsync(a => a.UserName == username && a.PassWord == password);
                if (account != null)
                {
                    // Logic to handle successful login
                    // Có thể thiết lập session hoặc cookie ở đây
                    HttpContext.Session.SetInt32("AccountId", account.AccountId);
                    if (account.Role == "Admin")
                    {
                        return RedirectToAction("IndexAdmin", "Home");
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Sai Tài Khoản Hoặc Mật Khẩu.");
                }
            }
            return View();
        }


        // GET: Accounts/Create
        public IActionResult DangKy()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DangKy([Bind("FullName,UserName,PassWord,Email,PhoneNumber,Role")] Account account, string re_password)
        {
            // Kiểm tra tính hợp lệ của model
            if (!ModelState.IsValid)
            {
                return View(account);
            }
            var FullNameExists = await _context.Accounts.AnyAsync(a => a.FullName == account.FullName);
            if (FullNameExists)
            {
                ModelState.AddModelError(string.Empty, "FullName đã tồn tại.");
                return View(account);
            }
            // Kiểm tra xem mật khẩu nhập lại có khớp không
            if (account.PassWord != re_password)
            {
                ModelState.AddModelError(string.Empty, "Xác nhận mật khẩu không khớp.");
                return View(account);
            }
            var userlExists = await _context.Accounts.AnyAsync(a => a.UserName == account.UserName);
            if (userlExists)
            {
                ModelState.AddModelError(string.Empty, "UserName đã tồn tại.");
                return View(account);
            }
            // Kiểm tra xem email đã tồn tại chưa
            var emailExists = await _context.Accounts.AnyAsync(a => a.Email == account.Email);
            if (emailExists)
            {
                ModelState.AddModelError(string.Empty, "Email đã tồn tại.");
                return View(account);
            }
            // Kiểm tra xem số điện thoại đã tồn tại chưa
            var phoneExists = await _context.Accounts.AnyAsync(a => a.PhoneNumber == account.PhoneNumber);
            if (phoneExists)
            {
                ModelState.AddModelError(string.Empty, "Số điện thoại đã tồn tại.");
                return View(account);
            }
            // Thêm tài khoản mới vào database
            _context.Add(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Login));
        }


        // GET: Accounts/Edit/5
        public async Task<IActionResult> QuenMatKhau()
        {
            var accountId = HttpContext.Session.GetInt32("AccountId");
            ViewBag.AccountId = accountId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> QuenMatKhau(string sdt, string email, string password)
        {
            var accountId = HttpContext.Session.GetInt32("AccountId");
            ViewBag.AccountId = accountId;
            if (ModelState.IsValid)
            {
                var account = await _context.Accounts
                    .FirstOrDefaultAsync(acc => acc.PhoneNumber == sdt && acc.Email == email);
                if (account != null)
                {
                    account.PassWord = password;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Login));
                }
                ModelState.AddModelError(string.Empty, "Số điện thoại hoặc email không hợp lệ.");
            }
            return View();
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.AccountId == id);
        }

        [HttpGet]
        public async Task<IActionResult> AccountDetail(int id)
        {
            var accountId = HttpContext.Session.GetInt32("AccountId");
            ViewBag.AccountId = accountId;
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AccountDetail(int id, Account model)
        {
            if (id != model.AccountId)
            {
                return BadRequest();
            }
                var account = await _context.Accounts.FindAsync(id);
                if (account == null)
                {
                    return NotFound();
                }
                account.FullName = model.FullName;
                account.Email = model.Email;
                account.PhoneNumber = model.PhoneNumber;
                account.Adress = model.Adress;
                account.DateOfBirth = model.DateOfBirth;
                account.Gender = model.Gender;
                await _context.SaveChangesAsync();
                return RedirectToAction("AccountDetail", new { id = id });              
        }
        public IActionResult Logout()

        {

            // Clear the session

            HttpContext.Session.Clear();

            // Redirect to the login page

            return RedirectToAction("Login", "Accounts");

        }
        [HttpGet]

        public IActionResult ChangePassword()

        {
            var accountId = HttpContext.Session.GetInt32("AccountId");
            ViewBag.AccountId = accountId;
            return View();

        }



        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword, string confirmNewPassword)
        {
            // Get the logged-in user ID from the session

            var accountId = HttpContext.Session.GetInt32("AccountId");
            ViewBag.AccountId = accountId;
            ViewBag.AccountId = accountId;  // Truyền AccountId qua ViewBag
            if (accountId == null)
            {
                return RedirectToAction("Login", "Accounts");
            }
            // Retrieve the logged-in user
            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null)
            {
                return NotFound();
            }
            // Validate the current password
            if (account.PassWord != currentPassword)
            {
                ModelState.AddModelError(string.Empty, "Mật khẩu hiện tại không đúng.");
                TempData["AccountId"] = accountId;
                ViewBag.AccountId = accountId;  // Truyền AccountId qua ViewBag
                return View();
            }
            // Validate that the new password and confirm password match
            if (newPassword != confirmNewPassword)
            {
                ModelState.AddModelError(string.Empty, "Mật khẩu mới và xác nhận mật khẩu không khớp.");
                TempData["AccountId"] = accountId;
                ViewBag.AccountId = accountId;  // Truyền AccountId qua ViewBag
                return View();
            }
            // Update the password
            account.PassWord = newPassword;
            await _context.SaveChangesAsync();
            // Optionally, log out the user after password change
            HttpContext.Session.Clear();
            // Redirect to the login page
            return RedirectToAction("Login", "Accounts");
        }




    }
}
