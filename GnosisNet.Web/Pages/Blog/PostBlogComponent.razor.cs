using GnosisNet.Web.Models;
using Microsoft.AspNetCore.Components;

namespace GnosisNet.Web.Pages.Blog
{
    public partial class PostBlogComponent
    {
        private BlogDto blog = new BlogDto();
        private string errorMessage;
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        private async Task HandleSubmit()
        {
            var response = await _blogService.AddBlog(blog);
            NavigationManager.NavigateTo("/Blog");
        }
    }
}
