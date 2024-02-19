using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnosisNet.Service.Models
{
    public class LoginRequestDto
    {
        [DefaultValue("")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([A-Za-z0-9][^'!&\\#*$%^?<>()=:;`~\[\]{}|/,₹€@ ][a-zA-z0-9-._][^!&\\#*$%^?<>()=:;`~\[\]{}|/,₹€@ ]*\@[a-zA-Z0-9.-]+(\.[A-Z|a-z]{2,6}){1,2})$", ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
