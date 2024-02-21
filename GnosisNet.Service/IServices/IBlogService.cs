using GnosisNet.Entities.Entities;
using GnosisNet.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnosisNet.Service.IServices
{
    public interface IBlogService
    {
        Task<ResponseDto<IEnumerable<BlogDto>>> GetAllBlogs();
        Task<ResponseDto<BlogDto>> GetBlogById(Guid id);
        Task<ResponseDto<BlogDto>> AddBlog(BlogDto blogDto);
        Task<ResponseDto<BlogDto>> UpdateBlog(Guid id, BlogDto blog);
        Task<ResponseDto<bool>> DeleteBlog(Guid id);
        Task<ResponseDto<List<BlogDto>>> GetBlogsByUser(string id);
    }
}
