using GnosisNet.Web.IServices;
using GnosisNet.Web.Models;
using GnosisNet.Web.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Reflection.Metadata;

namespace GnosisNet.Web.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBaseService _baseService;
        public BlogService(IBaseService baseService)
        {

            _baseService = baseService;

        }
        public async Task<ResponseDto> AddBlog(BlogDto blogDto)
        {
            var result = await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.POST,
                Data = blogDto,
                Url = "api/Blog"
            });
            return result;
        }

        public Task<bool> DeleteBlog(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BlogDto>> GetAllBlogs()
        {
            var blogResponse = await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                Url = "api/Blog"
            });
            if(blogResponse.IsSuccess)
            {
                var blogResult = JsonConvert.DeserializeObject<List<BlogDto>>(blogResponse?.Result.ToString());
                return blogResult;
            }
            return new List<BlogDto>();
        }

        public async Task<BlogDto> GetBlogById(string id)
        {
            var blogResponse = await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                Url = "api/Blog/" + id
            });
            if (blogResponse.IsSuccess)
            {
                var blogResult = JsonConvert.DeserializeObject<BlogDto>(blogResponse?.Result.ToString());
                return blogResult;
            }
            return new BlogDto();
        }

        public async Task<BlogDto> UpdateBlog(string id, BlogDto blog)
        {
            var blogResponse = await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.PUT,
                Url = "api/Blog/" + id,
                Data = blog
            });
            if (blogResponse.IsSuccess)
            {
                var blogResult = JsonConvert.DeserializeObject<BlogDto>(blogResponse?.Result.ToString());
                return blogResult;
            }
            return new BlogDto();
        }
        public async Task<List<BlogDto>> GetBlogsByUser()
        {
            var blogResponse = await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                Url = "api/Blog/getblogsbyuser"
            });
            if (blogResponse.IsSuccess)
            {
                var blogResult = JsonConvert.DeserializeObject<List<BlogDto>>(blogResponse?.Result.ToString());
                return blogResult;
            }
            return new List<BlogDto>();
        }
    }
}
