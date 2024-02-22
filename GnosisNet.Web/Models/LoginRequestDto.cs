using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GnosisNet.Web.Models
{
    public class LoginRequestDto
    {
        [DefaultValue("")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([A-Za-z0-9][^'!&\\#*$%^?<>()=:;`~\[\]{}|/,₹€@ ][a-zA-z0-9-._][^!&\\#*$%^?<>()=:;`~\[\]{}|/,₹€@ ]*\@[a-zA-Z0-9.-]+(\.[A-Z|a-z]{2,6}){1,2})$", ErrorMessage = "Please enter a valid email")]
        [Required]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}
