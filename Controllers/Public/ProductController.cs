using asp_mvc.Data;
using asp_mvc.Dtos;
using asp_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp_mvc.Controllers.Public
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

         [HttpGet]
        [Route("Public/Product/ProductList")]
        public IActionResult ProductList(int? SubCategoryId)
        {
            if (SubCategoryId == null || SubCategoryId == 0)
                return NotFound();

            var categoryId = _db.SubCategories
                            .Where(sc => sc.SubCategoryId == SubCategoryId)
                            .Select(sc => sc.CategoryId)
                            .FirstOrDefault();

            var subCategories = _db.SubCategories
                            .Where(sc => sc.CategoryId == categoryId)
                            .ToList();

            var productList = _db.Products.Where(p => p.SubCategoryId == SubCategoryId).ToList();

            var viewModel = new ProductListViewModel
            {
                Products = productList,
                SubCategories = subCategories
            };

            return View(viewModel);
        }

        [HttpGet]
        [Route("Public/Product/Search")]
    public IActionResult Search(string searchTerm)
    {
        var products = _db.Products
            .Where(p => p.Title.Contains(searchTerm) || p.Description.Contains(searchTerm))
            .ToList();
        
        return View(products);
    }

        [HttpGet]
        [Route("Public/Product/ProductDetail")]
        public IActionResult ProductDetail(int? ProductId)
        {
            var product = _db.Products
            .Include(p => p.SubCategory)
            .Include(p => p.Publisher)
            .FirstOrDefault(p => p.ProductId == ProductId);

        var reviews = _db.Reviews
            .Where(r => r.ProductId == ProductId)
            .Include(r => r.User)
            .ToList();

        if (product == null)
        {
            return NotFound();
        }

        var viewModel = new ProductDetailViewModel
        {
            Product = product,
            Reviews = reviews
        };

        return View(viewModel);
        }

        [Route("Public/Product/AddReview")]
        [HttpPost]
public IActionResult AddReview(ReviewViewModel model)
{
    if (!ModelState.IsValid)
    {
        TempData["ErrorMessage"] = "Invalid review submission.";
        return RedirectToAction("Details", new { id = model.ProductId });
    }

     var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "You need to log in to add products to your cart.";
                return RedirectToAction("Login", "Auth");
            }

    var review = new Review
    {
        ProductId = model.ProductId,
        UserId = (int)userId,
        Rating = model.Rating,
        Comment = model.Comment,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
    };

    _db.Reviews.Add(review);
    _db.SaveChanges();

    TempData["SuccessMessage"] = "Your review has been submitted.";
    return RedirectToAction("Details", new { id = model.ProductId });
}

        [HttpPost]
        [Route("Public/Product/AddToCart")]
        public IActionResult AddToCart(int? ProductId, int Quantity)
        {
            var product = _db.Products.Find(ProductId);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Product not found.";
                return RedirectToAction("ProductDetail", new { ProductId });
            }
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "You need to log in to add products to your cart.";
                return RedirectToAction("Login", "Auth");
            }
            var cart = new Cart
            {
                UserId = (int)userId,
                TotalPrice = (double)((product.PriceNew - (product.PriceNew * (product.Discount/100))) * Quantity),
                Status = "pending",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _db.Carts.Add(cart);
            _db.SaveChanges();

            var carItem = new CartItem
            {
                ProductId = (int)ProductId,
                Quantity = Quantity,
                Price = (double)(product.PriceNew - (product.PriceNew * (product.Discount/100))),
                CartId = cart.CartId
            };

            _db.CartItems.Add(carItem);
            _db.SaveChanges();

            product.Stock = product.Stock - Quantity;
            product.Sold = product.Sold + Quantity;
            _db.SaveChanges();

            TempData["SuccessMessage"] = "Product added to cart successfully!";
            return RedirectToAction("ProductDetail", new { ProductId });
        }
    }
}
