using PurrfectBlog.Web.Models.Dtos;

namespace PurrfectBlog.Web.ViewModels
{
  public class PostSummaryViewModel
  {
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Category { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Excerpt { get; set; } = string.Empty;

    public static PostSummaryViewModel FromDto(PostSummaryDto dto)
    {
      return new PostSummaryViewModel
      {
        Id = dto.Id,
        Title = dto.Title,
        Category = dto.Category,
        CreatedAt = dto.CreatedAt,
        UpdatedAt = dto.UpdatedAt,
        Excerpt = dto.Excerpt
      };
    }
  }
}