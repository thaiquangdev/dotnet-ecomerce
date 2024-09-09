using asp_mvc.Data;
using asp_mvc.Dtos;
using asp_mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace asp_mvc.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("Public/User/EditProfile")]
        public IActionResult EditProfile()
        {
            return View();
        }

        [Route("Public/User/EditProfile")]
        [HttpPost]
        public IActionResult EditProfile(EditProfileViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var email = HttpContext.Session.GetString("UserEmail");
            if (email == null)
            {
                TempData["ErrorMessage"] = "You need to log in to add products to your cart.";
                return RedirectToAction("Login", "Auth");
            }

            var userExists = _db.Users.Where(s => s.Email == email).FirstOrDefault();
            if (userExists == null)
            {
                TempData["ErrorMessage"] = "User does not exist.";
                return RedirectToAction("Login", "Auth");
            }

            
                userExists.UserName = model.UserName;
                userExists.PhoneNumber = model.PhoneNumber;
                userExists.FullName = model.FullName;

            _db.SaveChanges();
            TempData["SuccessMessage"] = "Password changed successfully.";
            return View();

        }

        [Route("Public/User/ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Route("Public/User/ChangePassword")]
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var email = HttpContext.Session.GetString("UserEmail");
            if (email == null)
            {
                TempData["ErrorMessage"] = "You need to log in to add products to your cart.";
                return RedirectToAction("Login", "Auth");
            }

            var userExists = _db.Users.Where(s => s.Email == email).FirstOrDefault();
            if (userExists == null)
            {
                TempData["ErrorMessage"] = "User does not exist.";
                return RedirectToAction("Login", "Auth");
            }

            if(!BCrypt.Net.BCrypt.Verify(model.OldPassword, userExists.Password))
            {
                TempData["ErrorMessage"] = "Old password is incorrect.";
                return RedirectToAction("ChangePassword");
            }

            userExists.Password = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            _db.SaveChanges();

            TempData["SuccessMessage"] = "Password changed successfully.";
            return View();
        }

        
    }
}