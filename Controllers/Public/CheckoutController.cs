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
                .Where(c => c.UserId == userId)
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

        [Route("Public/Checkout/CompletePurchase")]
        [HttpPost]
        public IActionResult CompletePurchase(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Xử lý lỗi khi dữ liệu không hợp lệ
                return View("Index", model);
            }

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "User is not logged in.";
                return RedirectToAction("Login", "Auth");
            }

            var shippingDetail = new ShippingDetail
            {
                UserId = (int)userId,
                CartId = model.CartId,
                Address = model.Address,
                City = model.City,
                ZipCode = model.ZipCode,
                Country = model.Country
            };

            _db.ShippingDetails.Add(shippingDetail);

            var payment = new Payment
            {
                UserId = (int)userId,
                CartId = model.CartId,
                Amount = model.Amount
            };

            _db.Payments.Add(payment);
            _db.SaveChanges();

            TempData["SuccessMessage"] = "Checkout is successful.";
            return RedirectToAction("Index", "Home");
        }
    }
}