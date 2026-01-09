using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PurrfectBlog.Web.Models;
using PurrfectBlog.Web.Services;
using PurrfectBlog.Web.ViewModels;

namespace PurrfectBlog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogService _blogService;

        public HomeController(ILogger<HomeController> logger, IBlogService blogService)
        {
            _logger = logger;
            _blogService = blogService;
        }

        public async Task<IActionResult> Index()
        {
            var recentPosts = await _blogService.GetRecentPostsAsync(3);
            var viewModels = recentPosts.Select(PostSummaryViewModel.FromEntity).ToList();
            return View(viewModels);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}