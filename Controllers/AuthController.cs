using asp_mvc.Data;
using asp_mvc.Dtos;
using asp_mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace asp_mvc.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        public AuthController(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

       

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterAuthViewModel model)
        {
            if(ModelState.IsValid)
            {
                var emailExist = _db.Users.FirstOrDefault(u => u.Email == model.Email);
                if(emailExist != null)
                {
                    TempData["ErrorMessage"] = "Email already exists.";
                    return View(model);
                }
                var user = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                    PhoneNumber = model.PhoneNumber,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _db.Users.Add(user);
                _db.SaveChanges();
                var role = _db.Roles.FirstOrDefault(r => r.RoleName == "User");
                if(role != null)
                {
                    var userRole = new UserRole
                    {
                        UserId = user.UserId,
                        RoleId = role.RoleId
                    };
                    _db.UserRoles.Add(userRole);
                    _db.SaveChanges();
                }

                
                TempData["SuccessMessage"] = "User registered successfully!";
                return RedirectToAction("Login", "Auth");
            }
            TempData["ErrorMessage"] = "There was a problem creating the user.";
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginAuthViewModel model)
        {
            if(ModelState.IsValid)
            {
                var emailExist = _db.Users.FirstOrDefault(u => u.Email == model.Email);
                
                if(emailExist != null && BCrypt.Net.BCrypt.Verify(model.Password, emailExist.Password))
                {

                    HttpContext.Session.SetInt32("UserId", emailExist.UserId);
                    HttpContext.Session.SetString("UserName", emailExist.UserName);
                    HttpContext.Session.SetString("UserEmail", emailExist.Email);

                    TempData["SuccessMessage"] = "Login successful!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ErrorMessage"] = "Email is not found.";
                    return View(model);
                }
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserEmail");
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("UserName");

            return RedirectToAction("Login", "Auth");
        }
    }
}