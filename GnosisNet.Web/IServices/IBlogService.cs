using GnosisNet.Web.Models;

namespace GnosisNet.Web.IServices
{
    public interface IBlogService
    {
        Task<List<BlogDto>> GetAllBlogs();
        Task<BlogDto> GetBlogById(string id);
        Task<ResponseDto> AddBlog(BlogDto blogDto);
        Task<BlogDto> UpdateBlog(string id, BlogDto blog);
        Task<bool> DeleteBlog(string id);
        Task<List<BlogDto>> GetBlogsByUser();
    }
}
