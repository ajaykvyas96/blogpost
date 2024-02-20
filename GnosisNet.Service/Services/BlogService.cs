using AutoMapper;
using GnosisNet.Entities.Entities;
using GnosisNet.Repository.Interface;
using GnosisNet.Service.IServices;
using GnosisNet.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnosisNet.Service.Services
{
    public class BlogService : IBlogService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BlogService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BlogDto>> GetAllBlogs()
        {
            var result = await _unitOfWork.Repository<Blog>().ListAllAsync();
            var blogs = _mapper.Map<IReadOnlyList<Blog>, List<BlogDto>>(result);
            return blogs;
        }

        public async Task<BlogDto> GetBlogById(Guid id)
        {
            var existingBlog = await _unitOfWork.Repository<Blog>().GetByIdAsync(id);
            var blog = _mapper.Map<Blog, BlogDto> (existingBlog);
            return blog;
        }

        public async Task<BlogDto> AddBlog(BlogDto blogDto)
        {
            var blog = _mapper.Map<BlogDto, Blog>(blogDto);
            blog.CreatedBy = "Admin";
            await _unitOfWork.Repository<Blog>().AddAsync(blog, "");
            await _unitOfWork.Complete();
            return blogDto;
        }

        public async Task<BlogDto> UpdateBlog(Guid id, BlogDto blog)
        {
            var existingBlog = await _unitOfWork.Repository<Blog>().GetByIdAsync(id);
            if (existingBlog != null)
            {
                existingBlog.Title = blog.Title;
                existingBlog.PostBody = blog.PostBody;
                existingBlog.Status = blog.Status;
                await _unitOfWork.Repository<Blog>().UpdateAsync(existingBlog, "Admin");
                await _unitOfWork.Complete();
                return blog;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteBlog(Guid id)
        {
            var existingBlog = await _unitOfWork.Repository<Blog>().GetByIdAsync(id);
            await _unitOfWork.Repository<Blog>().DeleteAsync(existingBlog, "Admin");
            return true;
        }
    }
}
