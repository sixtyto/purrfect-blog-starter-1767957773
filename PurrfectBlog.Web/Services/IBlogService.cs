using PurrfectBlog.Web.Models;

namespace PurrfectBlog.Web.Services
{
    public interface IBlogService
    {
        Task AddPostAsync(BlogPost post);
        Task<List<BlogPost>> GetAllPostsAsync();
    }
}
