using asp_mvc.Data;
using asp_mvc.Dtos;
using asp_mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace asp_mvc.Controllers.Admin
{
    public class PublisherController :Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PublisherController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        [Route("Admin/Publisher/Index")]
        public IActionResult Index()
        {
            List<Publisher> publishers = _db.Publishers.ToList();
            return View(publishers);
        }

        [Route("Admin/Publisher/Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Admin/Publisher/Create")]
        public IActionResult Create(CreatePublisherViewModel model)
        {
            if(ModelState.IsValid)
            {
                string image = null;
                string thumb = null;
                if(model.PublisherImage != null || model.PublisherImage.Length > 0)
                {
                    var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    image = Path.Combine(uploads, Path.GetFileName(model.PublisherImage.FileName));
                    using(var fileStream = new FileStream(image, FileMode.Create))
                    {
                        model.PublisherImage.CopyTo(fileStream);
                    }
                    image = "/images/" + Path.GetFileName(model.PublisherImage.FileName);
                }
                if(model.PublisherThumb != null || model.PublisherThumb.Length > 0)
                {
                    var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "thumbs");
                    thumb = Path.Combine(uploads, Path.GetFileName(model.PublisherThumb.FileName));
                    using(var fileStream = new FileStream(thumb, FileMode.Create))
                    {
                        model.PublisherThumb.CopyTo(fileStream);
                    }
                    thumb = "/thumbs/" + Path.GetFileName(model.PublisherThumb.FileName);
                }
                var publisher = new Publisher
                {
                    PublisherName = model.PublisherName,
                    PublisherImage = image,
                    PublisherThumb = thumb,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _db.Publishers.Add(publisher);
                _db.SaveChanges();
                TempData["SuccessMessage"] = "Publisher created successfully!";
                return RedirectToAction("Index", "Publisher");
            }
            return View(model);
        }
    }
}