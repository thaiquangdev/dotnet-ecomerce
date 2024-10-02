using asp_mvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp_mvc.Controllers.Admin 
{
    public class CartController : Controller 
    {
        private readonly ApplicationDbContext _db;
        public CartController(ApplicationDbContext db) {
            _db = db;
        } 

        [Route("Admin/Cart/Index")]
        public IActionResult Index()
        {   
            // Lấy danh sách đơn hàng cùng với chi tiết sản phẩm trong từng đơn hàng
            var carts = _db.Carts
                    .Include(c => c.CartItems) // Bao gồm các CartItems liên kết với mỗi đơn hàng
                    .ThenInclude(ci => ci.Product)  // Bao gồm cả thông tin sản phẩm cho mỗi CartItem
                    .Include(c => c.User)
                    .ToList();

            return View(carts); // Truyền dữ liệu đơn hàng sang view để hiển thị
        }
    }
}