using GnosisNet.Web.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace GnosisNet.Web.Models
{
    public class BlogDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string PostBody { get; set; }
        public DateTime? PublishedDate { get; set; }
        public BlogStatusEnum Status { get; set; }
    }
}
