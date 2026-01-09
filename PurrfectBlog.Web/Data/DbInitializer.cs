using Microsoft.EntityFrameworkCore;
using PurrfectBlog.Web.Models;

namespace PurrfectBlog.Web.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.Migrate();

            // Look for any posts.
            if (context.BlogPosts.Any())
            {
                return;   // DB has been seeded
            }

            var posts = new BlogPost[]
            {
                new BlogPost
                {
                    Title = "Why Cats Are Superior",
                    Content = "It is a scientifically proven fact that cats rule the internet. Their aloofness combined with sudden bursts of energy makes them the perfect companions.",
                    Category = "Science",
                    CreatedAt = DateTime.UtcNow.AddDays(-2)
                },
                new BlogPost
                {
                    Title = "Top 5 Nap Spots",
                    Content = "1. Keyboard\n2. Clean Laundry\n3. Inside a Box\n4. Sunbeam\n5. Human's Face",
                    Category = "Lifestyle",
                    CreatedAt = DateTime.UtcNow.AddDays(-1)
                },
                new BlogPost
                {
                    Title = "The Red Dot Conspiracy",
                    Content = "I have been chasing the red dot for years. I suspect it is an alien technology controlled by the dog next door.",
                    Category = "Conspiracy",
                    CreatedAt = DateTime.UtcNow
                }
            };

            context.BlogPosts.AddRange(posts);
            context.SaveChanges();
        }
    }
}