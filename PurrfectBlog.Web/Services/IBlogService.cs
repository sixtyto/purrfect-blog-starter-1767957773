using PurrfectBlog.Web.Models;

namespace PurrfectBlog.Web.Services
{
  public interface IBlogService
  {
    Task AddPostAsync(BlogPost post);
    Task<PagedResult<BlogPost>> GetPostsAsync(int page, int pageSize);
    Task<BlogPost?> GetPostByIdAsync(int id);
    Task<List<BlogPost>> GetRecentPostsAsync(int count);
    Task UpdatePostAsync(BlogPost post);
    Task DeletePostAsync(int id);
  }
}