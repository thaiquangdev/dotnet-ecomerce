using asp_mvc.Data;
using asp_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace asp_mvc.Controllers.Admin
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db) {
            _db = db;
        } 

        [Route("Admin/Category/Index")]
        public IActionResult Index() 
        {
            List<Category> categories = _db.Categories.ToList();
            return View(categories);
        }

        [Route("Admin/Category/Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Admin/Category/Create")]
        public IActionResult Create(Category obj)
        {
                if (!ModelState.IsValid)
    {
        // Ghi log lỗi nếu cần
        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            // Log lỗi
            Console.WriteLine($"Error: {error.ErrorMessage}");
        }

        
    }
            if(ModelState.IsValid)
            {
                obj.CreatedAt = DateTime.Now;
                obj.UpdatedAt = DateTime.Now;
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["SuccessMessage"] = "Category created successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "There was a problem creating the category.";
            }
            return View();
        }

        [Route("Admin/Category/Edit/{id}")]
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0) return NotFound();
            Category categoryFromDb = _db.Categories.Find(id);
            if(categoryFromDb == null) return NotFound();
            return View(categoryFromDb); 
        }

        [HttpPost]
        [Route("Admin/Category/Edit")]
        public IActionResult Edit(Category obj)
        {
            if(ModelState.IsValid)
            {
                obj.UpdatedAt = DateTime.Now;
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["SuccessMessage"] = "Category updated successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "There was a problem updating the category.";
            }
            return View();
        }

        [Route("Admin/Category/Delete/{id?}")]
        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0) return NotFound();
            Category categoryFromDb = _db.Categories.Find(id);
            if(categoryFromDb == null) return NotFound();
            return View(categoryFromDb); 
        }

        [HttpPost, ActionName("Delete")]
        [Route("Admin/Category/Delete/{id?}")]
        public IActionResult DeletePOST(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["ErrorMessage"] = "Category not found!";
                return RedirectToAction("Index");
            }

            Category categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                TempData["ErrorMessage"] = "Category not found!";
                return RedirectToAction("Index");
            }

            _db.Categories.Remove(categoryFromDb);
            _db.SaveChanges();
            TempData["SuccessMessage"] = "Category deleted successfully!";
            return RedirectToAction("Index");
        }

    }
}