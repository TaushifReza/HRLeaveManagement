using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace Hr.LeaveManagement.BlazorUI.Services.Base;

public class BaseHttpService
{
    protected readonly IClient _client;
    protected readonly ILocalStorageService _localStorageService;

    public BaseHttpService(IClient client, ILocalStorageService localStorageService)
    {
        _client = client;
        _localStorageService = localStorageService;
    }

    protected Response<Guid> ConvertApiExceptions<Guid>(ApiException ex)
    {
        if (ex.StatusCode == 400)
        {
            return new Response<Guid>()
            {
                Message = "Invalid data was submitted",
                ValidationErrors = ex.Response,
                Success = false
            };
        }
        else if (ex.StatusCode == 404)
        {
            return new Response<Guid>()
            {
                Message = "The record was not found",
                Success = false
            };
        }
        else
        {
            return new Response<Guid>()
            {
                Message = "Something went wrong, please try again later",
                Success = false
            };
        }
    }

    protected async Task AddBearerToken()
    {
        if (await _localStorageService.ContainKeyAsync("token"))
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", await _localStorageService.GetItemAsync<string>("token"));
    }
}