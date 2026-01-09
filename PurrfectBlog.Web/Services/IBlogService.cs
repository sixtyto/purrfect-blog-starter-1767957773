using PurrfectBlog.Web.Models;
using PurrfectBlog.Web.Models.Dtos;

namespace PurrfectBlog.Web.Services
{
  public interface IBlogService
  {
    Task AddPostAsync(CreatePostDto createDto);
    Task<PagedResult<PostSummaryDto>> GetPostsAsync(int page, int pageSize);
    Task<PostDto?> GetPostByIdAsync(int id);
    Task<List<PostSummaryDto>> GetRecentPostsAsync(int count);
    Task<bool> UpdatePostAsync(UpdatePostDto updateDto);
    Task<bool> DeletePostAsync(int id);
  }
}