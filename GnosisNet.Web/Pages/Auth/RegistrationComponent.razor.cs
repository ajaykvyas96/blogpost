using GnosisNet.Web.Models;
using Microsoft.AspNetCore.Components;

namespace GnosisNet.Web.Pages.Auth
{
    public partial class RegistrationComponent
    {
        private RegistrationRequestDto registrationModel = new RegistrationRequestDto();
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        private string errorMessage;
        private async Task HandleSubmit()
        {
            try
            {
                var authResponse = await _authService.Register(registrationModel);
                if(authResponse.IsSuccess == true)
                {
                    NavigationManager.NavigateTo("/login");
                }
                errorMessage = authResponse.Message;

                // Redirect to login page after successful registration
                
            }
            catch (Exception ex)
            {
                errorMessage = "An error occurred while processing your request.";
                // Log the exception or handle it as needed
            }
        }
    }
}
