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
            var query = _context.BlogPosts.AsNoTracking().OrderByDescending(p => p.CreatedAt);
            
            var totalCount = await query.CountAsync();
            var skip = Math.Max(0, (page - 1) * pageSize);
            var items = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<BlogPost>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = page,
                PageSize = pageSize
            };
        }

        public async Task<BlogPost?> GetPostByIdAsync(int id)
        {
            return await _context.BlogPosts.FindAsync(id);
        }

        public async Task<List<BlogPost>> GetRecentPostsAsync(int count)
        {
            return await _context.BlogPosts
                .OrderByDescending(p => p.CreatedAt)
                .Take(count)
                .ToListAsync();
        }
    }
}