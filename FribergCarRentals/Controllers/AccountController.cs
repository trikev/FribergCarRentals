using FribergCarRentals.ViewModels;
using Microsoft.AspNetCore.Mvc;
using FribergCarRentals.Models;
using FribergCarRentals.Data;

namespace FribergCarRentals.Controllers
{

    public class AccountController : Controller
    {
        private readonly IUser userRepository;

        public AccountController(IUser userRepository)
        {
            this.userRepository = userRepository;
        }

        // GET: Account/Login
        public ActionResult Login(string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View("LoginCreateAccount");
            }
            else
            {
                return View();
            }
            
        }

        
        //POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind("Email", "Password")] LoginViewModel loginViewModel, string? returnUrl)
        {
            if(ModelState.IsValid)
            {
                var user = userRepository.GetUserByEmailAndPassword(loginViewModel.Email, loginViewModel.Password);
                if(user != null)
                {
                    SetupUserSession(user);
                    return HandleRedirect(user, returnUrl);
                }
            }
            ViewBag.FailedLogin = "Det finns inget konto som matchar dina angivna uppgifter.";
            if(returnUrl != null)
            {
                return View("LoginCreateAccount");
            }
            return View();
        }
        
        public void SetupUserSession(User user)
        {   
            HttpContext.Session.SetString("IsAuthenticated", "True");
            HttpContext.Session.SetString("UserId", user.UserId.ToString());
            HttpContext.Session.SetString("RoleId", user.Role.RoleName);
        }

        
        public ActionResult HandleRedirect(User user, string returnUrl)
        {
            
            if(user.RoleId == 1)
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            else if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else if(user.RoleId == 2)
            {
                return RedirectToAction("HomePage", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/Logout
   
        public ActionResult Logout() 
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }

}



