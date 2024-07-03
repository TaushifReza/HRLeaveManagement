using Hr.LeaveManagement.BlazorUI.Contracts;
using Hr.LeaveManagement.BlazorUI.Models;
using Microsoft.AspNetCore.Components;

namespace Hr.LeaveManagement.BlazorUI.Pages
{
    public partial class Register
    {
        public RegisterVM Model { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public string Message { get; set; }
        [Inject]
        private IAuthService AuthService { get; set; }

        protected override void OnInitialized()
        {
            Model = new RegisterVM();
        }

        protected async Task HandleRegister()
        {
            var result = await AuthService.RegisterAsync(Model.FirstName, Model.LastName, Model.UserName, Model.Email,
                Model.Password);
            if (result)
            {
                NavigationManager.NavigateTo("/");
            }

            Message = "Something went wrong, Please try Again";
        }
    }
}
