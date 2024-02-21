using GnosisNet.Web.IServices;
using GnosisNet.Web.Models;
using GnosisNet.Web.Services;
using Microsoft.AspNetCore.Components;

namespace GnosisNet.Web.Pages.Blog
{
    public partial class BlogDetailComponent
    {
        [Parameter]
        public string Id { get; set; }
        private BlogDto blog;
        [Inject]
        NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            blog = await _blogService.GetBlogById(Id);
        }

        private void GoBack()
        {
            NavigationManager.NavigateTo("/");
        }
    }
}
