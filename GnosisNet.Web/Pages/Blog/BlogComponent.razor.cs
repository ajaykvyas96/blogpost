using GnosisNet.Web.Models;
using Microsoft.AspNetCore.Components;

namespace GnosisNet.Web.Pages.Blog
{
    public partial class BlogComponent
    {
        public List<BlogDto>? blogs;
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            blogs = await _blogService.GetAllBlogs();
        }

        private async Task NavigateToFullPost(Guid id)
        {
            // Navigate to the full blog post page, passing the BlogId as a route parameter
            NavigationManager.NavigateTo($"/Blog/{id}");
        }
    }
}
