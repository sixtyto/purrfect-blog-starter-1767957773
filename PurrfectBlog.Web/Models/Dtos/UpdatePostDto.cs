namespace PurrfectBlog.Web.Models.Dtos
{
  public class UpdatePostDto
  {
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public string? Category { get; set; }
  }
}