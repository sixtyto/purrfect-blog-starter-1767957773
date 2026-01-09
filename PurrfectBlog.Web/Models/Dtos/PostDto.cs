namespace PurrfectBlog.Web.Models.Dtos
{
  public class PostDto
  {
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public string? Category { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
  }
}