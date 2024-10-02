using asp_mvc.Data;
using asp_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp_mvc.Controllers.Admin
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Route("Admin/User/Index")]
        public IActionResult Index()
        {
            List<User> user = _db.Users.Include(sc => sc.UserRoles).ThenInclude(ur => ur.Role).ToList();
            return View(user);
        }

        [HttpGet]
        [Route("Admin/User/Create")]
        public IActionResult Create()
        {
            return View();
        }


        [Route("Admin/User/Create")]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _db.Users.Add(user);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
    
            ViewBag.Roles = _db.Roles.ToList(); // Reload roles if model is invalid
            return View(user);
        }
    }
}