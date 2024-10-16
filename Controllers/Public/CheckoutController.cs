using asp_mvc.Data;
using asp_mvc.Dtos;
using asp_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp_mvc.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CheckoutController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("Public/Checkout/Index")]
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) 
            {
                TempData["ErrorMessage"] = "Bạn cần đăng nhập để xem giỏ hàng.";
                return RedirectToAction("Login", "Auth");
            }

            var carts = _db.Carts
                .Where(c => c.UserId == userId && c.Status == "pending")
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .ToList();

            if (!carts.Any())
            {
                TempData["ErrorMessage"] = "Giỏ hàng của bạn đang trống.";
                return RedirectToAction("Index", "Home");
            }

            var checkoutViewModel = new CheckoutViewModel
            {
                CartItems = carts.SelectMany(c => c.CartItems.Select(ci => new CartItemViewModel
                {
                    ProductId = ci.ProductId,
                    Title = ci.Product.Title,
                    Price = ci.Price,
                    Quantity = ci.Quantity,
                    Thumb = ci.Product.Thumb,
                    Discount = (int)ci.Product.Discount
                })).ToList(),
                Amount = carts.Sum(c => c.CartItems.Sum(ci => ci.Price * ci.Quantity)),
                CartId = carts.FirstOrDefault()?.CartId ?? 0 // Giả sử có một giỏ hàng duy nhất cho mỗi người dùng
            };

            return View(checkoutViewModel);
        }

        [HttpPost]
        [Route("Public/Checkout/CompletePurchase")]
        public IActionResult CompletePurchase(CheckoutViewModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "Bạn cần đăng nhập để thực hiện thanh toán.";
                return RedirectToAction("Login", "Auth");
            }

            // Tạo thông tin giao hàng từ dữ liệu của model
            var shippingDetail = new ShippingDetail
            {
                UserId = (int)userId,
                CartId = model.CartId,
                Address = model.Address,
                City = model.City,
                ZipCode = model.ZipCode,
                Country = model.Country,
                State = "pending" // Trạng thái ban đầu
            };

            // Lưu thông tin giao hàng vào cơ sở dữ liệu
            _db.ShippingDetails.Add(shippingDetail);

            // Tạo thông tin thanh toán từ dữ liệu của model
            var payment = new Payment
            {
                UserId = (int)userId,
                CartId = model.CartId,
                Amount = model.Amount,
                PaymentDate = DateTime.Now,
                PaymentMethod = "CARD", // Phương thức thanh toán, bạn có thể thay đổi tùy theo yêu cầu
                PaymentStatus = "success" // Trạng thái thanh toán thành công
            };

            // Lưu thông tin thanh toán vào cơ sở dữ liệu
            _db.Payments.Add(payment);

            // Cập nhật trạng thái giỏ hàng
            var cart = _db.Carts.SingleOrDefault(c => c.CartId == model.CartId);
            if (cart != null)
            {
                cart.Status = "success"; // Đánh dấu giỏ hàng là đã hoàn thành
                _db.SaveChanges();
            }

            // Lưu tất cả các thay đổi vào cơ sở dữ liệu
            _db.SaveChanges();

            TempData["SuccessMessage"] = "Thanh toán thành công.";
            return RedirectToAction("Index", "Home");
        }
    }
}