using System;

namespace PurrfectBlog.Web.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public string? Category { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
