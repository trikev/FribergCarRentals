using FribergCarRentals.Data;
using FribergCarRentals.Models;
using FribergCarRentals.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FribergCarRentals.Controllers
{
    public class UserController : Controller
    {
        private readonly IUser userRepository;
        private readonly IRole roleRepository;

        public UserController(IUser userRepository, IRole roleRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }

        
        //GET: User/CreateUserAccount
        public ActionResult CreateUserAccount(string returnUrl)
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

        //POST: User/CreateUserAccount/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUserAccount(CreateUserViewModel createUserVM, string? returnUrl)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    if(userRepository.HasEmailOrPassword(createUserVM.Email, createUserVM.Password))
                    {
                        ViewBag.AlreadyExists = "Konto finns redan.";
                        return View();
                    }

                    User user = new User
                    {
                        Email = createUserVM.Email,
                        Password = createUserVM.Password,
                        UserName = createUserVM.UserName,
                        RoleId = createUserVM.RoleId,
                        Role = roleRepository.GetRoleById(createUserVM.RoleId)

                    };
                    userRepository.Add(user);

                    SetupUserSession(user);
                    return HandleRedirect(user, returnUrl);

                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return NotFound();
            }
        }
        
        public void SetupUserSession(User user)
        {
            HttpContext.Session.SetString("IsAuthenticated", "True");
            HttpContext.Session.SetString("UserId", user.UserId.ToString());
            HttpContext.Session.SetString("RoleId", user.Role.RoleName);
        }

        //Sköter redirect om Role = user || admin
        public ActionResult HandleRedirect(User user, string returnUrl)
        {

            if (user.RoleId == 1)
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            else if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else if (user.RoleId == 2)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
        
    }
}
