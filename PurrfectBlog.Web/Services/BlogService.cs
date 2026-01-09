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

        public async Task<List<BlogPost>> GetAllPostsAsync()
        {
            return await _context.BlogPosts
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }
    }
}
