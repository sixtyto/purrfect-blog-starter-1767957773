namespace PurrfectBlog.Web.ViewModels
{
    public class PostDetailsViewModel
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public string? Category { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
