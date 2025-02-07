using FribergCarRentals.Data;
using FribergCarRentals.Models;
using FribergCarRentals.ViewModels;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using NuGet.Packaging.Rules;
//using System.Diagnostics;
//using System.Linq.Expressions;
//using Microsoft.AspNetCore.Http;

namespace FribergCarRentals.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUser userRepository;
        private readonly IRole roleRepository;

        public UserController(IUser userRepository, IRole roleRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }



        //GET: Admin/UserController/AllUsers
        public ActionResult AllUsers()
        {
            return View(userRepository.GetAllUsers());
        }

        //GET: Admin/UserController/AddUSer
        public ActionResult AddUser()
        {
            return View();
        }
        
        //POST: Admin/UserController/AddUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(CreateUserViewModel userVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var role= roleRepository.GetRoleById(userVM.RoleId);

                    User user = new User
                    {
                        UserName = userVM.UserName,
                        Email = userVM.Email,
                        Password = userVM.Password,
                        RoleId = userVM.RoleId,
                        Role = role
                    };
                    userRepository.Add(user);
                    return RedirectToAction("AllUsers");
                }
                return View("Index");
            }
            catch
            {
                return NotFound();
            }
        }
       

        //GET: Admin/UserController/EditUser/5
        public ActionResult EditUser(int userId)
        {
            return View(userRepository.GetUserById(userId));
        } 

        //POST: Admin/UserController/EditUser/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(User user)
        {
           
            try
            {
                if (ModelState.IsValid)
                {
                    user.Role = roleRepository.GetRoleById(user.RoleId);
                    userRepository.Update(user);
                }
                return RedirectToAction("AllUsers");
            }
            catch
            {
                return View();
            }
            
            
        }

        //GET: Admin/UserController/RemoveUser/5
        public ActionResult RemoveUser(int userId)
        {
            return View(userRepository.GetUserById(userId));
        }

        //POST: Admin/UserController/RemoveUser/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveUser(User user)
        {
            try
            {
                userRepository.Delete(user);
                return RedirectToAction("AllUsers");
            }
            catch
            {

                return View();
            }
        }

    }
}
