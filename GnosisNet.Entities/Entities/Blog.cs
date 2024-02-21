using GnosisNet.Entities.Entities.Enums;
using GnosisNet.Entities.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnosisNet.Entities.Entities
{
    public class Blog : BaseEntity
    {
        [Column("title")]
        [Required]
        public string Title { get; set; }
        [Column("postbody")]
        [Required]
        public string PostBody { get; set; }
        [Column("publisheddate")]
        public DateTime? PublishedDate { get; set; }
        [Column("status")]
        public BlogStatusEnum Status { get; set; }
        public ApplicationUser User { get; set; }
    }
}
