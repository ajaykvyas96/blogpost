using Azure;
using GnosisNet.API.Extensions;
using GnosisNet.Entities.Entities;
using GnosisNet.Repository.Interface;
using GnosisNet.Service.IServices;
using GnosisNet.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace GnosisNet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BlogController(IBlogService blogService, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _blogService = blogService;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }
        // GET: /api/blog
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _blogService.GetAllBlogs());
        }

        // GET: /api/blog/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _blogService.GetBlogById(id));
        }
        // GET: /api/blog/{id}
        [HttpGet("getblogsbyuser")]
        [Authorize]
        public async Task<IActionResult> GetBlogsByUser()
        {
            string? userid = _httpContextAccessor?.HttpContext?.GetCurrentUserId();
            return Ok(await _blogService.GetBlogsByUser(userid));
        }

        // POST: /api/blog
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] BlogDto blog)
        {
            var result = await _blogService.AddBlog(blog);
            if(result.IsSuccess)
            {
                string? userid = _httpContextAccessor?.HttpContext?.GetCurrentUserId();
                await _unitOfWork.Complete(userid);
            }
            return Ok(result);
        }

        // PUT: /api/blog/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(Guid id, [FromBody] BlogDto blog)
        {
            var result = await _blogService.UpdateBlog(id, blog);
            if (result.IsSuccess)
            {
                string? userid = _httpContextAccessor?.HttpContext?.GetCurrentUserId();
                await _unitOfWork.Complete(userid);
            }
            return Ok(result);
        }

        // DELETE: /api/blog/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _blogService.DeleteBlog(id);
            if (result.IsSuccess)
            {
                string? userid = _httpContextAccessor?.HttpContext?.GetCurrentUserId();
                await _unitOfWork.Complete(userid);
            }
            return Ok(result);
        }
    }
}
