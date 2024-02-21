using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnosisNet.Entities.Entities
{
    public class BaseEntity
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("createdon"), DataType(DataType.Date), Required]
        public DateTime CreatedOn { get; set; }

        [Column("createdby"), Required, StringLength(450)]
        public string CreatedBy { get; set; }

        [Column("updatedby"), StringLength(450)]
        public string? UpdatedBy { get; set; }

        [Column("updatedon")]
        public DateTime? UpdatedOn { get; set; }

        [Column("isactive")]
        public bool IsActive { get; set; }
    }
}
