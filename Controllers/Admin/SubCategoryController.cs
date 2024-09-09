using asp_mvc.Data;
using asp_mvc.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp_mvc.Controllers.Admin
{
    public class SubCategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<SubCategoryController> _logger;
        public SubCategoryController(ApplicationDbContext db, ILogger<SubCategoryController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [Route("Admin/SubCategory/Index")]
        public IActionResult Index()
        {
            List<SubCategory> subCategories = _db.SubCategories.Include(sc => sc.Category).ToList();
            return View(subCategories);
        }

       [HttpGet]
       [Route("Admin/SubCategory/Create")]
        public IActionResult Create()
        {
            var viewModel = new SubCategoryViewModel
            {
                Categories = _db.Categories.ToList() // Lấy danh sách categories
            };

            return View(viewModel);
        }

        [HttpPost]
        [Route("Admin/SubCategory/Create")]
        public IActionResult Create(SubCategoryViewModel model)
        {
   
            if (!ModelState.IsValid)
            {
                // Ghi log lỗi nếu cần
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    // Log lỗi
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }

                 // Trả lại View với dữ liệu ban đầu để người dùng có thể sửa chữa
                model.Categories = _db.Categories.ToList(); // Load lại danh sách categories
                return View(model);
            }
            if (ModelState.IsValid)
            {
                var subCategory = new SubCategory
                {
                    SubCategoryName = model.SubCategoryName,
                    CategoryId = model.CategoryId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

            _db.SubCategories.Add(subCategory);
            _db.SaveChanges();

            TempData["SuccessMessage"] = "Sub Category created successfully!";
            return RedirectToAction("Index");
            }

        // Nếu không hợp lệ, lấy lại danh sách categories
        model.Categories = _db.Categories.ToList();
        return View(model);
        }

   
        [HttpGet]
        [Route("Admin/SubCategory/Edit/{id}")]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            SubCategory subCategoryFromDb = _db.SubCategories.Find(id);
            if (subCategoryFromDb == null)
                return NotFound();

            var viewModel = new EditSubCategoryViewModel
            {
                SubCategoryName = subCategoryFromDb.SubCategoryName,
                CategoryId = subCategoryFromDb.CategoryId,
                Categories = _db.Categories.ToList(),
                UpdatedAt = subCategoryFromDb.UpdatedAt
            };

            return View(viewModel);
        }

        [HttpPost]
        [Route("Admin/SubCategory/Edit/{id}")]
        public IActionResult Edit(int id, EditSubCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var subCategoryFromDb = _db.SubCategories.Find(id);
                if (subCategoryFromDb == null)
                    return NotFound();

                subCategoryFromDb.SubCategoryName = model.SubCategoryName;
                subCategoryFromDb.CategoryId = model.CategoryId;
                subCategoryFromDb.UpdatedAt = DateTime.Now;

                _db.SubCategories.Update(subCategoryFromDb);
                _db.SaveChanges();

                TempData["SuccessMessage"] = "Sub Category updated successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                model.Categories = _db.Categories.ToList();
                TempData["ErrorMessage"] = "There was a problem updating the subcategory.";
            }
            return View(model);
        }

        [HttpGet]
        [Route("Admin/SubCategory/Delete/{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            SubCategory subCategoryFromDb = _db.SubCategories.Find(id);
            if (subCategoryFromDb == null)
                return NotFound();

            

            return View(subCategoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        [Route("Admin/SubCategory/Delete/{id}")]
        public IActionResult DeletePOST(int id)
        {
            var subCategoryFromDb = _db.SubCategories.Find(id);
            if (subCategoryFromDb == null)
                return NotFound();

            _db.SubCategories.Remove(subCategoryFromDb);
            _db.SaveChanges();

            TempData["SuccessMessage"] = "Sub Category deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}