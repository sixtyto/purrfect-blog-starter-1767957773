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

        [HttpGet("Posts")]
        public async Task<IActionResult> Index(int page = 1)
        {
            page = Math.Max(1, page);

            const int pageSize = 5;
            var result = await _blogService.GetPostsAsync(page, pageSize);
            
            var viewModel = new PagedResult<PostSummaryViewModel>
            {
                Items = result.Items.Select(PostSummaryViewModel.FromEntity).ToList(),
                TotalCount = result.TotalCount,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };

            return View(viewModel);
        }

        [HttpGet("Posts/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var post = await _blogService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            var viewModel = new PostDetailsViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Category = post.Category,
                CreatedAt = post.CreatedAt
            };

            return View(viewModel);
        }

        [HttpGet("CreatePost")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("CreatePost")]
        [ValidateAntiForgeryToken]
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

            return RedirectToAction("Index");
        }
    }
}
