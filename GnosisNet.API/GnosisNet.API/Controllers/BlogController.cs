using Azure;
using GnosisNet.Entities.Entities;
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
        private ResponseDto _response;
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService, ResponseDto response)
        {
            _blogService = blogService;
            _response = response;

        }
        // GET: /api/blog
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var blog = await _blogService.GetAllBlogs();
            if(blog == null)
            {
                return NotFound();
            }
            _response.Result = blog;
            _response.IsSuccess = true;
            return Ok(_response);
        }

        // GET: /api/blog/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var blog = await _blogService.GetBlogById(id);
            if (blog == null)
            {
                return NotFound();
            }
            _response.Result = blog;
            _response.IsSuccess = true;
            return Ok(_response);
        }

        // POST: /api/blog
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] BlogDto blog)
        {
            var result = await _blogService.AddBlog(blog);
            _response.Result = result;
            _response.IsSuccess = true;
            _response.Message = "Blog created successfully.";
            return Ok(_response);
        }

        // PUT: /api/blog/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(Guid id, [FromBody] BlogDto blog)
        {
            var result = await _blogService.UpdateBlog(id, blog);
            if (result == null)
            {
                return NotFound();
            }
            _response.Result = result;
            _response.IsSuccess = true;
            _response.Message = "Blog updated successfully.";
            return Ok(_response);
        }

        // DELETE: /api/blog/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _blogService.DeleteBlog(id);
            if (result == null)
            {
                return NotFound();
            }
            _response.Result = result;
            _response.IsSuccess = true;
            _response.Message = "Blog deleted successfully.";
            return Ok(_response);
        }
    }
}
