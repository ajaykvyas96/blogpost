using GnosisNet.Web.Models;

namespace GnosisNet.Web.IServices
{
    public interface IBlogService
    {
        Task<List<BlogDto>> GetAllBlogs();
        Task<BlogDto> GetBlogById(string id);
        Task<ResponseDto> AddBlog(BlogDto blogDto);
        Task<BlogDto> UpdateBlog(Guid id, BlogDto blog);
        Task<bool> DeleteBlog(Guid id);
    }
}
