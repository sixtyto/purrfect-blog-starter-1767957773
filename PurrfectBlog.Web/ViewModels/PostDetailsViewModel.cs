using PurrfectBlog.Web.Models.Dtos;

namespace PurrfectBlog.Web.ViewModels
{
  public class PostDetailsViewModel
  {
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public string? Category { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public static PostDetailsViewModel FromDto(PostDto dto)
    {
      return new PostDetailsViewModel
      {
        Id = dto.Id,
        Title = dto.Title,
        Content = dto.Content,
        Category = dto.Category,
        CreatedAt = dto.CreatedAt,
        UpdatedAt = dto.UpdatedAt
      };
    }
  }
}