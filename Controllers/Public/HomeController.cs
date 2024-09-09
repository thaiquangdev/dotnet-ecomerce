using asp_mvc.Data;
using asp_mvc.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace asp_mvc.Controllers.Public
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("Public/Home/Index")]
        public IActionResult Index()
        {
            var categories = _db.Categories.Include(c => c.SubCategories).ToList();
            var publishers = _db.Publishers.ToList();
            var products = _db.Products.OrderByDescending(p => p.Discount).Take(6).ToList();
            var productSolds = _db.Products.OrderByDescending(p => p.Sold).Take(6).ToList();
            var viewModel = new HomeViewModel
            {
                Categories = categories,
                Publishers = publishers,
                Products = products,
                ProductSolds = productSolds
            };
            return View(viewModel);
        }
    }
}