using asp_mvc.Data;
using asp_mvc.Dtos;
using asp_mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace asp_mvc.Controllers.Admin
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment) 
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        [Route("Admin/Product/Index")]
        public IActionResult Index( int page = 1, int pageSize = 10)
        {
            List<Product> products = _db.Products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var totalProducts = _db.Products.Count();
            var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);
             var viewModel = new ProductAdminViewModel
            {
                Products = products,
                CurrentPage = page,
                TotalPages = totalPages
            };
            return View(viewModel);
        }

        [Route("Admin/Product/Create")]
        public IActionResult Create()
        {
            var viewModel = new CreateProductViewModel
            {
                SubCategories = _db.SubCategories.ToList(),
                Publishers = _db.Publishers.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [Route("Admin/Product/Create")]
        public IActionResult Create(CreateProductViewModel model)
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
                model.SubCategories = _db.SubCategories.ToList(); // Load lại danh sách categories
                return View(model);
            }
            if(ModelState.IsValid)
            {
                string imageUrl = null;
                string thumb = null;
                if(model.ImageUrl != null ||model.ImageUrl.Length > 0)
                {
                    var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    imageUrl = Path.Combine(uploads, Path.GetFileName(model.ImageUrl.FileName));
                    using (var fileStream = new FileStream(imageUrl, FileMode.Create))
                    {
                        model.ImageUrl.CopyTo(fileStream);
                    }
                    imageUrl = "/images/" + Path.GetFileName(model.ImageUrl.FileName);
                }
                 if (model.Thumb != null && model.Thumb.Length > 0)
                {
                    var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "thumbs");
                    thumb = Path.Combine(uploads, Path.GetFileName(model.Thumb.FileName));
                    using (var fileStream = new FileStream(thumb, FileMode.Create))
                    {
                        model.Thumb.CopyTo(fileStream);
                    }
                    thumb = "/thumbs/" + Path.GetFileName(model.Thumb.FileName);
                }
                var product = new Product
                {
                    Title = model.Title,
                    Description = model.Description,
                    ImageUrl = imageUrl,
                    Thumb = thumb,
                    Discount = model.Discount,
                    PriceNew = model.PriceNew,
                    Stock = model.Stock,
                    Sold = model.Sold,
                    Author = model.Author,
                    SubCategoryId = model.SubCategoryId,
                    PublisherId = model.PublisherId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _db.Products.Add(product);
                _db.SaveChanges();

                TempData["SuccessMessage"] = "Product created successfully!";
                return RedirectToAction("Index", "Product");
            }
            model.SubCategories = _db.SubCategories.ToList();
            return View(model);
        }

        [Route("Admin/Product/Edit/{id}")]
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0) return NotFound();
            Product product = _db.Products.Find(id);
            if(product == null) return NotFound();
            var viewModel = new EditProductViewModel
            {
                Title = product.Title,
                Description = product.Description,
                Discount = product.Discount,
                PriceNew = product.PriceNew,
                Stock = product.Stock,
                Author = product.Author,
                SubCategoryId = product.SubCategoryId,
                UpdatedAt = product.UpdatedAt,
                SubCategories = _db.SubCategories.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [Route("Admin/Product/Edit/{id}")]
        public IActionResult Edit(int? id, EditProductViewModel model)
        {
            if(ModelState.IsValid)
            {
                Product product = _db.Products.Find(id);
                if(product == null) return NotFound();

                product.Title = model.Title;
                product.Description = model.Description;
                product.Discount = model.Discount;
                product.PriceNew = model.PriceNew;
                product.Stock = model.Stock;
                product.Author = model.Author;
                product.UpdatedAt = DateTime.Now;

                _db.Products.Update(product);
                _db.SaveChanges();
                TempData["SuccessMessage"] = "Product updated successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                model.SubCategories = _db.SubCategories.ToList();
                TempData["ErrorMessage"] = "There was a problem updating the product.";
            }
            return View(model);
        }

        [Route("Admin/Product/Delete/{id}")]
        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0) return NotFound();
            Product product = _db.Products.Find(id);
            if(product == null) return NotFound();
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [Route("Admin/Product/Delete/{id}")]
        public IActionResult DeletePost(int? id)
        {
            if(id == null || id == 0) return NotFound();
            Product product = _db.Products.Find(id);
            if(product == null) return NotFound();
            _db.Products.Remove(product);
            _db.SaveChanges();
            TempData["SuccessMessage"] = "Product deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}