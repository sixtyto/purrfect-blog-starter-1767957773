using Microsoft.EntityFrameworkCore;

using PurrfectBlog.Web.Data;
using PurrfectBlog.Web.Models;

namespace PurrfectBlog.Web.Services
{
  public class BlogService : IBlogService
  {
    private readonly ApplicationDbContext _context;

    public BlogService(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task AddPostAsync(BlogPost post)
    {
      _context.BlogPosts.Add(post);
      await _context.SaveChangesAsync();
    }

    public async Task<PagedResult<BlogPost>> GetPostsAsync(int page, int pageSize)
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
          .ToListAsync();

      return new PagedResult<BlogPost>
      {
        Items = items,
        TotalCount = totalCount,
        PageNumber = actualPage,
        PageSize = pageSize
      };
    }

    public async Task<BlogPost?> GetPostByIdAsync(int id)
    {
      return await _context.BlogPosts
          .AsNoTracking()
          .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<BlogPost>> GetRecentPostsAsync(int count)
    {
      return await _context.BlogPosts
          .OrderByDescending(p => p.CreatedAt)
          .Take(count)
          .AsNoTracking()
          .ToListAsync();
    }

    public async Task<bool> UpdatePostAsync(int id, string title, string content, string? category)
    {
      var existingPost = await _context.BlogPosts.FindAsync(id);
      if (existingPost == null)
      {
        return false;
      }

      existingPost.Title = title;
      existingPost.Content = content;
      existingPost.Category = category;
      existingPost.UpdatedAt = DateTime.UtcNow;

      await _context.SaveChangesAsync();
      return true;
    }

    public async Task<bool> DeletePostAsync(int id)
    {
      var post = await _context.BlogPosts.FindAsync(id);
      if (post == null)
      {
        return false;
      }

      _context.BlogPosts.Remove(post);
      await _context.SaveChangesAsync();
      return true;
    }
  }
}