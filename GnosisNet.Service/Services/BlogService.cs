using AutoMapper;
using GnosisNet.Data;
using GnosisNet.Entities.Entities;
using GnosisNet.Entities.Entities.Enums;
using GnosisNet.Entities.Entities.Identity;
using GnosisNet.Repository.Interface;
using GnosisNet.Service.IServices;
using GnosisNet.Service.Models;
using Microsoft.EntityFrameworkCore;
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
        private readonly GnosisDbContext _context;

        public BlogService(IUnitOfWork unitOfWork, IMapper mapper, GnosisDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ResponseDto<IEnumerable<BlogDto>>> GetAllBlogs()
        {
            //var result = await _unitOfWork.Repository<Blog>().ListAllAsync();
            //var blogs = _mapper.Map<IReadOnlyList<Blog>, List<BlogDto>>(result);
            var blogs = await _context.Blogs
                .Include(b => b.User)
                .Where(b => b.Status == BlogStatusEnum.Published)
                .Select(b => new BlogDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    PostBody = b.PostBody,
                    PublishedDate = b.PublishedDate,
                    PublishedBy = b.User.FirstName + " " + b.User.LastName
                }).OrderByDescending(x => x.PublishedDate)
                .ToListAsync();
            return new ResponseDto<IEnumerable<BlogDto>>()
            {
                Result = blogs
            };
        }

        public async Task<ResponseDto<BlogDto>> GetBlogById(Guid id)
        {
            var existingBlog = await _unitOfWork.Repository<Blog>().GetByIdAsync(id);
            var user  = await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == existingBlog.CreatedBy);
            var blog = _mapper.Map<Blog, BlogDto>(existingBlog);
            blog.PublishedBy = user.FirstName + " " + user.LastName;
            return new ResponseDto<BlogDto>()
            {
                Result = blog
            };
        }

        public async Task<ResponseDto<BlogDto>> AddBlog(BlogDto blogDto)
        {
            var blog = _mapper.Map<BlogDto, Blog>(blogDto);
            blog.CreatedBy = "Admin";
            await _unitOfWork.Repository<Blog>().AddAsync(blog, "");
            return new ResponseDto<BlogDto>()
            {
                Result = blogDto,
                Message = "Blog created successfully"
            };
        }

        public async Task<ResponseDto<BlogDto>> UpdateBlog(Guid id, BlogDto blogDto)
        {
            var existingBlog = await _unitOfWork.Repository<Blog>().GetByIdAsync(id);
            if (existingBlog != null)
            {
                existingBlog.Title = blogDto.Title;
                existingBlog.PostBody = blogDto.PostBody;
                existingBlog.Status = blogDto.Status;
                await _unitOfWork.Repository<Blog>().UpdateAsync(existingBlog, "Admin");
                return new ResponseDto<BlogDto>()
                {
                    Result = blogDto,
                    Message = "Blog updated successfully"
                };
            }
            else
            {
                return new ResponseDto<BlogDto>() { Message = "No record found", IsSuccess = false };
            }
        }

        public async Task<ResponseDto<bool>> DeleteBlog(Guid id)
        {
            var existingBlog = await _unitOfWork.Repository<Blog>().GetByIdAsync(id);
            if (existingBlog != null)
            {
                await _unitOfWork.Repository<Blog>().DeleteAsync(existingBlog, "Admin");
                return new ResponseDto<bool>()
                {
                    Message = "Records has been deleted succefully",
                    Result = true
                }; 
            }
            else
            {
                return new ResponseDto<bool>() { IsSuccess = false, Message = "No Record found" };
            }
        }

        public async Task<ResponseDto<List<BlogDto>>> GetBlogsByUser(string id)
        {
            var blogs = await _unitOfWork.Repository<Blog>().Query(x => x.CreatedBy == id);
            var blogsList = _mapper.Map<IReadOnlyList<Blog>, List<BlogDto>>(blogs.ToList());
            blogsList = blogsList.OrderByDescending(x => x.PublishedDate).ToList();
            return new ResponseDto<List<BlogDto>>()
            {
                Result = blogsList
            };
        }
    }
}
