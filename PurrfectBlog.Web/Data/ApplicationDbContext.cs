using Microsoft.EntityFrameworkCore;

using PurrfectBlog.Web.Models;

namespace PurrfectBlog.Web.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<BlogPost> BlogPosts { get; set; }
  }
}