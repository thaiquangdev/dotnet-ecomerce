using Microsoft.AspNetCore.Mvc;
using asp_mvc.Data;

namespace asp_mvc.Controllers.Admin;

public class DashboardController : Controller
{
    private readonly ApplicationDbContext _db;

        public DashboardController(ApplicationDbContext db)
        {
            _db = db;
        }

    

    [Route("Admin/Dashboard/Index")]
        public IActionResult Index()
        {
            var totalUsers = _db.Users.Count();

            

            var totalOrders = _db.Carts.Count();

            var pendingOrders = _db.Carts.Count(c => c.Status == "Pending");

            var totalRevenue = _db.Carts.Sum(c => c.TotalPrice);


            var statistics = new
            {
                TotalUsers = totalUsers,
                TotalOrders = totalOrders,
                PendingOrders = pendingOrders,
                TotalRevenue = totalRevenue,
            };

            return View(statistics); 
        }
}
