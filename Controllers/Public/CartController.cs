using asp_mvc.Data;
using asp_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp_mvc.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("Public/Cart/Index")]
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) 
            {
                TempData["ErrorMessage"] = "You need to log in to view your cart.";
                return RedirectToAction("Login", "Auth");
            }

            List<Cart> carts = _db.Carts
                .Where(c => c.UserId == userId)
                    .Include(c => c.CartItems)
                        .ThenInclude(ci => ci.Product)
                            .ThenInclude(p => p.Publisher)
                            .ToList();

            if (!carts.Any())
            {
                TempData["ErrorMessage"] = "Your cart is empty.";
                return RedirectToAction("Index", "Home");
            }

            return View(carts);
        }

        [Route("Public/Cart/UpdateQuantity")]
        public IActionResult UpdateQuantity(int cartItemId, int productId, int quantity) 
        {
            var cartItem = _db.CartItems.Find(cartItemId);
            if (cartItem == null)
            {
                return NotFound("CartItem not found");
            }

            var product = _db.Products.Find(productId);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            cartItem.Quantity = quantity;

            product.Stock = product.Stock - (quantity - cartItem.Quantity);
            product.Sold = product.Sold + (quantity - cartItem.Quantity);

            var cart = _db.Carts.Find(cartItem.CartId);
            if (cart != null)
            {
                cart.TotalPrice = _db.CartItems
                    .Where(ci => ci.CartId == cartItem.CartId)
                    .Sum(ci => ci.Price * ci.Quantity);
            }

            _db.SaveChanges();

            return RedirectToAction("Index", "Cart");
        }


        [Route("Public/Cart/DeleteCart")]
        [HttpPost]
        public IActionResult DeleteCart(int cartItemId)
        {
            var cartItem = _db.CartItems.Find(cartItemId);
            if (cartItem == null)
            {
                return NotFound("CartItem not found");
            }

            var product = _db.Products.Find(cartItem.ProductId);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            // Cập nhật lại stock và số lượng đã bán của sản phẩm
            product.Stock += cartItem.Quantity;
            product.Sold -= cartItem.Quantity;

            // Lấy giỏ hàng của người dùng
            var cart = _db.Carts.Find(cartItem.CartId);
            if(cart != null)
            {
                // Xóa item khỏi giỏ hàng
                _db.CartItems.Remove(cartItem);

                // Cập nhật lại tổng giá trị của giỏ hàng
                cart.TotalPrice = _db.CartItems
                    .Where(ci => ci.CartId == cart.CartId)
                    .Sum(ci => ci.Price * ci.Quantity);

                // Kiểm tra nếu giỏ hàng không còn item nào thì xóa giỏ hàng
                if (!_db.CartItems.Any(ci => ci.CartId == cart.CartId))
                {
                    _db.Carts.Remove(cart);
                }

                _db.SaveChanges();
            }

            return RedirectToAction("Index", "Cart");
        }

    }
}