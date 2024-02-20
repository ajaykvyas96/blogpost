namespace GnosisNet.Web.Models
{
    public class LoginResponseDto
    {
        public string? Token { get; set; }
        public UserDto User { get; set; }
    }
}
