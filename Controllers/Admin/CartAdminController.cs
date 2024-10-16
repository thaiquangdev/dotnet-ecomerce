using asp_mvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp_mvc.Controllers.Admin 
{
    public class CartAdminController : Controller 
    {
        private readonly ApplicationDbContext _db;
        
        public CartAdminController(ApplicationDbContext db) 
        {
            _db = db;
        }

        [Route("Admin/CartAdmin/Index")]
        public IActionResult Index()
        {   
            var carts = _db.Carts
                .Include(c => c.CartItems) 
                    .ThenInclude(ci => ci.Product) 
                .Include(c => c.User) 
                .ToList();

            return View(carts);
        }
    }
}