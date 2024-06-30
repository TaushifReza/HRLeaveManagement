namespace Hr.LeaveManagement.BlazorUI.Contracts
{
    public interface IAuthService
    {
        Task<bool> AuthenticateAsync(string email, string password);
        Task<bool> RegisterAsync(string firstName, string lastName, string userName, string email, string password);
        Task LogoutAsync();
    }
}
