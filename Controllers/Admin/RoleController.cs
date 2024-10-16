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

         // Hiển thị form Edit Role
    [HttpGet]
    [Route("Admin/Role/Edit")]
    public IActionResult Edit(int id)
    {
        var role = _db.Roles.FirstOrDefault(r => r.RoleId == id);
        if (role == null)
        {
            return NotFound();
        }
        return View(role);
    }

    // Xử lý post để cập nhật role
    [HttpPost]
    [Route("Admin/Role/Edit")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Role role)
    {
        if (ModelState.IsValid)
        {
            var roleInDb = _db.Roles.FirstOrDefault(r => r.RoleId == role.RoleId);
            if (roleInDb == null)
            {
                return NotFound();
            }

            // Cập nhật thông tin role
            roleInDb.RoleName = role.RoleName;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(role);
    }
    }
}