using asp_mvc.Data;
using asp_mvc.Models;
using Microsoft.AspNetCore.Mvc;

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
            List<User> user = _db.Users.ToList();
            return View(user);
        }
    }
}