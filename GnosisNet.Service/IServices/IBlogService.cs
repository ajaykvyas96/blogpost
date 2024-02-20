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
        Task<IEnumerable<BlogDto>> GetAllBlogs();
        Task<BlogDto> GetBlogById(Guid id);
        Task<BlogDto> AddBlog(BlogDto blogDto);
        Task<BlogDto> UpdateBlog(Guid id, BlogDto blog);
        Task<bool> DeleteBlog(Guid id);
    }
}
