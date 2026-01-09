using System.ComponentModel.DataAnnotations;

namespace PurrfectBlog.Web.ViewModels
{
  public class EditPostViewModel
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter a title for your masterpiece.")]
    [StringLength(100, ErrorMessage = "Title is too long! Keep it under 100 characters.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Don't leave us hanging! Write some content.")]
    [StringLength(10000, MinimumLength = 10, ErrorMessage = "Content must be between 10 and 10000 characters.")]
    public string Content { get; set; } = string.Empty;

    [Display(Name = "Category (Optional)")]
    [StringLength(50, ErrorMessage = "Category name is too long.")]
    public string? Category { get; set; }
  }
}