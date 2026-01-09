using Microsoft.AspNetCore.Mvc;

using PurrfectBlog.Web.Models;
using PurrfectBlog.Web.Models.Dtos;
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
        Items = result.Items.Select(dto => new PostSummaryViewModel
        {
          Id = dto.Id,
          Title = dto.Title,
          Category = dto.Category,
          CreatedAt = dto.CreatedAt,
          UpdatedAt = dto.UpdatedAt,
          Excerpt = dto.Excerpt
        }).ToList(),
        TotalCount = result.TotalCount,
        PageNumber = result.PageNumber,
        PageSize = result.PageSize
      };

      return View(viewModel);
    }

    [HttpGet("Posts/{id}")]
    public async Task<IActionResult> Details(int id)
    {
      var dto = await _blogService.GetPostByIdAsync(id);
      if (dto == null)
      {
        return NotFound();
      }

      var viewModel = new PostDetailsViewModel
      {
        Id = dto.Id,
        Title = dto.Title,
        Content = dto.Content,
        Category = dto.Category,
        CreatedAt = dto.CreatedAt,
        UpdatedAt = dto.UpdatedAt
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

      var createDto = new CreatePostDto
      {
        Title = model.Title,
        Content = model.Content,
        Category = model.Category
      };

      await _blogService.AddPostAsync(createDto);

      TempData["SuccessMessage"] = "Purr-fect! Your blog post has been published successfully. 🐾";

      return RedirectToAction("Index");
    }

    [HttpGet("EditPost/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
      var dto = await _blogService.GetPostByIdAsync(id);
      if (dto == null)
      {
        return NotFound();
      }

      var viewModel = new EditPostViewModel
      {
        Id = dto.Id,
        Title = dto.Title,
        Content = dto.Content,
        Category = dto.Category
      };

      return View(viewModel);
    }

    [HttpPost("EditPost/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EditPostViewModel model)
    {
      if (id != model.Id)
      {
        return BadRequest();
      }

      if (!ModelState.IsValid)
      {
        return View(model);
      }

      var updateDto = new UpdatePostDto
      {
        Id = model.Id,
        Title = model.Title,
        Content = model.Content,
        Category = model.Category
      };

      var success = await _blogService.UpdatePostAsync(updateDto);

      if (!success)
      {
        return NotFound();
      }

      TempData["SuccessMessage"] = "Post updated successfully! 📝";
      return RedirectToAction("Details", new { id = model.Id });
    }

    [HttpPost("DeletePost/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
      var success = await _blogService.DeletePostAsync(id);

      if (!success)
      {
        return NotFound();
      }

      TempData["SuccessMessage"] = "Post deleted successfully. 👋";
      return RedirectToAction("Index");
    }
  }
}