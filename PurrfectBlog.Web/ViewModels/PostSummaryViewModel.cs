using PurrfectBlog.Web.Models;

namespace PurrfectBlog.Web.ViewModels
{
    public class PostSummaryViewModel
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Category { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Excerpt { get; set; } = string.Empty;

        public static PostSummaryViewModel FromEntity(BlogPost post)
        {
            var excerpt = (post.Content?.Length ?? 0) > 150 
                ? post.Content!.Substring(0, 150) + "..." 
                : post.Content ?? string.Empty;

            return new PostSummaryViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Category = post.Category,
                CreatedAt = post.CreatedAt,
                Excerpt = excerpt
            };
        }
    }
}
