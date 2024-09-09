using asp_mvc.Data;
using asp_mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace asp_mvc.Controllers.Admin
{
    public class RoleController : Controller
    {
        private readonly ApplicationDbContext _db;
        public RoleController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Route("Admin/Role/Index")]
        public IActionResult Index()
        {
            List<Role> roles = _db.Roles.ToList();
            return View(roles);
        }
        [Route("Admin/Role/Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Admin/Role/Create")]
        public IActionResult Create(Role model)
        {
            if(ModelState.IsValid)
            {
                model.CreatedAt = DateTime.Now;
                model.UpdatedAt = DateTime.Now;
                _db.Roles.Add(model);
                _db.SaveChanges();
                TempData["SuccessMessage"] = "Role created successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "There was a problem creating the role.";
            }
            return View(model);
        }
    }
}