using GnosisNet.Web.Models;
using Microsoft.AspNetCore.Components;
using Telerik.Blazor;
using Telerik.Blazor.Components;
using Telerik.Blazor.Components.Editor;

namespace GnosisNet.Web.Pages.Blog
{
    public partial class PostBlogComponent
    {
        private BlogDto blog = new BlogDto()
        {
            PostBody = ""
        };
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
        private async Task HandleSubmit()
        {
            var response = await _blogService.AddBlog(blog);
            NavigationManager.NavigateTo("/myblogs");
        }
        private void GoBack()
        {
            NavigationManager.NavigateTo("/myblogs");
        }
    }
}
