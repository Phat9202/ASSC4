using Microsoft.EntityFrameworkCore;
using Test.Data;
using Test.Models;

namespace Test.Service
{
    public class AuthenticationService
    {
        private readonly MyDb _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private const string sessionUser = "User";

        public AuthenticationService(MyDb context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public bool Login(string username, string password)
        {
            var user = _context.Accounts.Include(x => x.UserRoles).ThenInclude(x => x.Role)
                .SingleOrDefault(x=>x.UserName==username||x.PasswordHash==password);
            if (user == null) 
            {
                return false;
            }
            //Lưu thông tin user vào sesion
            _contextAccessor.HttpContext.Session.SetObjectAsJson(sessionUser, user);
            return true;
        }

        public Account GetCurentUser()
        {
            return _contextAccessor.HttpContext.Session.GetObjectFromJson<Account>(sessionUser);
        }
    }
}
