using Microsoft.AspNetCore.Mvc;
using PurrfectBlog.Web.Models;
using PurrfectBlog.Web.Services;
using PurrfectBlog.Web.ViewModels;

namespace PurrfectBlog.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet("CreatePost")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("CreatePost")]
        public async Task<IActionResult> Create(CreatePostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var post = new BlogPost
            {
                Title = model.Title,
                Content = model.Content,
                Category = model.Category
            };

            await _blogService.AddPostAsync(post);

            TempData["SuccessMessage"] = "Purr-fect! Your blog post has been published successfully. 🐾";

            return RedirectToAction("Index", "Home");
        }
    }
}
