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

        [Route("Auth/Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Auth/Register")]
        public IActionResult Register(RegisterAuthViewModel model)
        {
            if (ModelState.IsValid)
            {
                var emailExist = _db.Users.FirstOrDefault(u => u.Email == model.Email);
                if (emailExist != null)
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
                if (role != null)
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

        [Route("Auth/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
[Route("Auth/Login")]
public IActionResult Login(LoginAuthViewModel model)
{
    if (ModelState.IsValid)
    {
        var user = _db.Users.FirstOrDefault(u => u.Email == model.Email);

        // Kiểm tra nếu user là null
        if (user == null)
        {
            TempData["ErrorMessage"] = "Email không tồn tại.";
            return View(model);
        }

        if (BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
        {
            // Lưu thông tin người dùng vào session
            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("UserName", user.UserName ?? string.Empty);
            HttpContext.Session.SetString("UserEmail", user.Email ?? string.Empty);

            // Kiểm tra vai trò của người dùng
            var roles = _db.UserRoles
                           .Where(ur => ur.UserId == user.UserId)
                           .Select(ur => ur.Role.RoleName)
                           .ToList();

            if (roles.Contains("Admin"))
            {
                TempData["SuccessMessage"] = "Đăng nhập thành công! Chào mừng Admin.";
                return RedirectToAction("Index", "Dashboard"); // Điều hướng đến Admin Dashboard
            }
            else
            {
                TempData["SuccessMessage"] = "Đăng nhập thành công! Chào mừng User.";
                return RedirectToAction("Index", "Home"); // Điều hướng đến trang chủ
            }
        }
        else
        {
            TempData["ErrorMessage"] = "Mật khẩu không chính xác.";
            return View(model);
        }
    }
    return View(model);
}

        [Route("Auth/Logout")]
        public IActionResult Logout()
        {
            // Xóa thông tin người dùng trong session
            HttpContext.Session.Clear();
            TempData["SuccessMessage"] = "You have been logged out.";
            return RedirectToAction("Login", "Auth");
        }
    }
}