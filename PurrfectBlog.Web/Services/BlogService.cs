using Microsoft.EntityFrameworkCore;

using PurrfectBlog.Web.Data;
using PurrfectBlog.Web.Models;
using PurrfectBlog.Web.Models.Dtos;

namespace PurrfectBlog.Web.Services
{
  public class BlogService : IBlogService
  {
    private readonly ApplicationDbContext _context;

    private static readonly System.Linq.Expressions.Expression<Func<BlogPost, PostSummaryDto>> _postSummaryProjection = p => new PostSummaryDto
    {
      Id = p.Id,
      Title = p.Title,
      Category = p.Category,
      CreatedAt = p.CreatedAt,
      UpdatedAt = p.UpdatedAt,
      Excerpt = p.Content.Length > 150 ? p.Content.Substring(0, 150) + "..." : p.Content
    };

    public BlogService(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task AddPostAsync(CreatePostDto createDto)
    {
      var post = new BlogPost
      {
        Title = createDto.Title,
        Content = createDto.Content,
        Category = createDto.Category
      };

      _context.BlogPosts.Add(post);
      await _context.SaveChangesAsync();
    }

    public async Task<PagedResult<PostSummaryDto>> GetPostsAsync(int page, int pageSize)
    {
      if (pageSize < 1)
      {
        pageSize = 10;
      }

      var query = _context.BlogPosts.AsNoTracking().OrderByDescending(p => p.CreatedAt);

      var totalCount = await query.CountAsync();
      var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

      var actualPage = Math.Max(1, Math.Min(page, Math.Max(1, totalPages)));

      var skip = (actualPage - 1) * pageSize;
      var items = await query
          .Skip(skip)
          .Take(pageSize)
          .Select(_postSummaryProjection)
          .ToListAsync();

      return new PagedResult<PostSummaryDto>
      {
        Items = items,
        TotalCount = totalCount,
        PageNumber = actualPage,
        PageSize = pageSize
      };
    }

    public async Task<PostDto?> GetPostByIdAsync(int id)
    {
      var post = await _context.BlogPosts
          .AsNoTracking()
          .FirstOrDefaultAsync(p => p.Id == id);

      if (post == null)
      {
        return null;
      }

      return new PostDto
      {
        Id = post.Id,
        Title = post.Title,
        Content = post.Content,
        Category = post.Category,
        CreatedAt = post.CreatedAt,
        UpdatedAt = post.UpdatedAt
      };
    }

    public async Task<List<PostSummaryDto>> GetRecentPostsAsync(int count)
    {
      return await _context.BlogPosts
          .OrderByDescending(p => p.CreatedAt)
          .Take(count)
          .AsNoTracking()
          .Select(_postSummaryProjection)
          .ToListAsync();
    }

    public async Task<bool> UpdatePostAsync(UpdatePostDto updateDto)
    {
      var affectedRows = await _context.BlogPosts
          .Where(p => p.Id == updateDto.Id)
          .ExecuteUpdateAsync(s => s
              .SetProperty(p => p.Title, updateDto.Title)
              .SetProperty(p => p.Content, updateDto.Content)
              .SetProperty(p => p.Category, updateDto.Category)
              .SetProperty(p => p.UpdatedAt, DateTime.UtcNow));
      return affectedRows > 0;
    }

    public async Task<bool> DeletePostAsync(int id)
    {
      var affectedRows = await _context.BlogPosts
          .Where(p => p.Id == id)
          .ExecuteDeleteAsync();
      return affectedRows > 0;
    }
  }
}