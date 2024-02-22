using GnosisNet.Web.Models.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GnosisNet.Web.Models
{
    public class BlogDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required(ErrorMessage = "Post is required")]
        public string PostBody { get; set; }
        public DateTime PublishedDate { get; set; }
        public string? PublishedBy { get; set; }
        public BlogStatusEnum Status { get; set; }
    }
}
