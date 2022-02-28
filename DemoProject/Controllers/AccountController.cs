using DemoProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace DemoProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly AdminUserContext _contextAdminUser;

        public AccountController(AdminUserContext contextAdminUser)
        {
            _contextAdminUser = contextAdminUser;
        }

        /// <summary>
        /// Login page.
        /// </summary>
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// POST: Login with user credentials.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind("UserId,UserName,Password")] AdminUser user)
        {
            if (ModelState.IsValid)
            {
                var loginUser = _contextAdminUser.AdminUsers
                .FirstOrDefault(m => m.UserName == user.UserName && m.Password == user.Password);
                if (loginUser == null)
                {
                    TempData["Notification"] = "Your username or password is wrong!";
                    return View();
                }
                //var identity = new System.Security.Principal.GenericIdentity(user.UserName);
                //var principal = new GenericPrincipal(identity, new string[0]);
                //HttpContext.Session.SetString("sessionName", user.UserName);
                //Thread.CurrentPrincipal = principal;
                return RedirectToAction("Configurations", "Home");
            }
            return View();
        }

        /// <summary>
        /// Register page.
        /// </summary>
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// POST: Create a user.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserId,UserName,Email,Password")] AdminUser user)
        {
            if (ModelState.IsValid)
            {

                var registeredUsername = _contextAdminUser.AdminUsers
                .FirstOrDefault(m => m.UserName == user.UserName);

                var registeredEmail = _contextAdminUser.AdminUsers
                .FirstOrDefault(m => m.Email == user.Email);

                if (registeredUsername == null && registeredEmail == null)
                {
                    _contextAdminUser.Add(user);
                    await _contextAdminUser.SaveChangesAsync();
                    TempData["AccountCreated"] = "Your account has been created!";
                    return RedirectToAction(nameof(Login));
                }
                else if (registeredUsername != null)
                {
                    TempData["Notification"] = "This username is already exist!";
                    return View();
                }
                else if (registeredEmail != null)
                {
                    TempData["Notification"] = "This email address is already exist!";
                    return View();
                }

            }
            return View();
        }

        /// <summary>
        /// Clear session data when sign out.
        /// </summary>
        //public IActionResult SessionOut()
        //{
        //    HttpContext.Session.Clear();
        //    return View("Login");
        //}

    }
}
