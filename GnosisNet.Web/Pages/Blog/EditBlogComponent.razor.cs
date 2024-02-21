using GnosisNet.Web.Models;
using Microsoft.AspNetCore.Components;
using Telerik.Blazor.Components.Editor;
using Telerik.Blazor.Components;

namespace GnosisNet.Web.Pages.Blog
{
    public partial class EditBlogComponent
    {
        [Parameter]
        public string Id { get; set; }
        private BlogDto blog;
        private string errorMessage;
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public List<IEditorTool> Tools { get; set; } =
        new List<IEditorTool>()
        {
            new EditorButtonGroup(new Telerik.Blazor.Components.Editor.Bold(), new Telerik.Blazor.Components.Editor.Italic(), new Telerik.Blazor.Components.Editor.Underline()),
            new EditorButtonGroup(new Telerik.Blazor.Components.Editor.AlignLeft(), new Telerik.Blazor.Components.Editor.AlignCenter(), new Telerik.Blazor.Components.Editor.AlignRight()),
            new UnorderedList(),
            new EditorButtonGroup(new CreateLink(), new Telerik.Blazor.Components.Editor.Unlink(), new InsertImage()),
            new InsertTable(),
            new EditorButtonGroup(new AddRowBefore(), new AddRowAfter(), new MergeCells(), new SplitCell()),
            new Format(),
            new Telerik.Blazor.Components.Editor.FontSize(),
            new Telerik.Blazor.Components.Editor.FontFamily()
        };
        protected override async Task OnInitializedAsync()
        {
            blog = await _blogService.GetBlogById(Id);
        }

        private async Task HandleSubmit()
        {
            var response = await _blogService.UpdateBlog(Id, blog);
            NavigationManager.NavigateTo("/myblogs");
        }
        private void GoBack()
        {
            NavigationManager.NavigateTo("/myblogs");
        }
    }
}
