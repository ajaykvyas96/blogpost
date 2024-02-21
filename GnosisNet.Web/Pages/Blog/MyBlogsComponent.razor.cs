using GnosisNet.Web.Models;
using Microsoft.AspNetCore.Components;

namespace GnosisNet.Web.Pages.Blog
{
    public partial class MyBlogsComponent
    {
        public List<BlogDto>? blogs;
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            blogs = await _blogService.GetBlogsByUser();
        }

        private async Task NavigateToFullPost(Guid id)
        {
            NavigationManager.NavigateTo($"/Blog/{id}");
        }
        private async Task NavigateToEditPost(Guid id)
        {
            NavigationManager.NavigateTo($"/Blog/Edit/{id}");
        }
        private async Task NavigateToCreateBlog()
        {
            NavigationManager.NavigateTo($"/Blog/create");
        }
    }
}
