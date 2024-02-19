using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnosisNet.Service.Models
{
    public class UserDto
    {
        public string ID { get; set; }
        public string Email { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
    }
}
