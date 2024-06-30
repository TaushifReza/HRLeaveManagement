using Blazored.LocalStorage;
using Hr.LeaveManagement.BlazorUI.Contracts;
using Hr.LeaveManagement.BlazorUI.Providers;
using Hr.LeaveManagement.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace Hr.LeaveManagement.BlazorUI.Services
{
    public class AuthService : BaseHttpService, IAuthService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthService(IClient client, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider) : base(client, localStorageService)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> AuthenticateAsync(string email, string password)
        {
            try
            {
                AuthRequest authRequest = new AuthRequest() { Email = email, Password = password };
                var authenticationResponse = await _client.LoginAsync(authRequest);
                if (authenticationResponse.Token != string.Empty)
                {
                    await _localStorageService.SetItemAsync("token", authenticationResponse.Token);

                    // Set Claims in Blazor and login state
                    await ((ApiAuthStateProvider)_authenticationStateProvider).LoggedIn();

                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> RegisterAsync(string firstName, string lastName, string userName, string email, string password)
        {
            RegistrationRequest registrationRequest = new RegistrationRequest()
                { FirstName = firstName, LastName = lastName, UserName = userName, Email = email, Password = password };
            var response = await _client.RegisterAsync(registrationRequest);
            if (!string.IsNullOrEmpty(response.UserId))
            {
                return true;
            }
            return false;
        }

        public async Task LogoutAsync()
        {
            // remove claims in Blazor and invalidate login state
            await ((ApiAuthStateProvider)_authenticationStateProvider).LoggedOut();
        }
    }
}
