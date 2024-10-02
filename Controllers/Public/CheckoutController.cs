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
                TempData["ErrorMessage"] = "You need to log in to view your cart.";
                return RedirectToAction("Login", "Auth");
            }

            var carts = _db.Carts
                .Where(c => c.UserId == userId && c.Status == "pending")
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .ToList();

            if (!carts.Any())
            {
                TempData["ErrorMessage"] = "Your cart is empty.";
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
                CartId = carts.FirstOrDefault()?.CartId ?? 0 // Assuming there is only one cart per user
            };

            return View(checkoutViewModel);
        }

        [HttpPost]
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
        State = "pending"
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
        PaymentMethod = "CARD",
        PaymentStatus = "success"
    };

    // Lưu thông tin thanh toán vào cơ sở dữ liệu
    _db.Payments.Add(payment);

    // Lưu các thay đổi vào cơ sở dữ liệu
    _db.SaveChanges();

    var cart = _db.Carts.SingleOrDefault(c => c.CartId == model.CartId);
    if (cart != null)
    {
        cart.Status = "success";
        _db.SaveChanges();
    }

    TempData["SuccessMessage"] = "Thanh toán thành công.";
    return RedirectToAction("Index", "Home");
}
    }
}