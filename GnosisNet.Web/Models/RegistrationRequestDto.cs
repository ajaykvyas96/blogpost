using System.ComponentModel.DataAnnotations;

namespace GnosisNet.Web.Models
{
    public class RegistrationRequestDto
    {
        [Required]
        public string Email { get; set; }
        [Required(ErrorMessage = "First name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
