using GnosisNet.Entities.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnosisNet.Service.Models
{
    public class BlogDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string PostBody { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string? PublishedBy { get; set; }
        public BlogStatusEnum Status { get; set; }
    }
}
