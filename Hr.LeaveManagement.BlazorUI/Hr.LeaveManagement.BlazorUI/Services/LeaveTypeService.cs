using AutoMapper;
using Blazored.LocalStorage;
using Hr.LeaveManagement.BlazorUI.Contracts;
using Hr.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Hr.LeaveManagement.BlazorUI.Services.Base;

namespace Hr.LeaveManagement.BlazorUI.Services
{
    public class LeaveTypeService : BaseHttpService, ILeaveTypeService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<LeaveTypeService> _logger;

        public LeaveTypeService(IClient client, IMapper mapper, ILocalStorageService localStorageService, ILogger<LeaveTypeService> logger) : base(client, localStorageService)
        {
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<LeaveTypeVM>> GetLeaveTypesAsync()
        {
            await AddBearerToken();
            var token = await _localStorageService.GetItemAsync<string>("token");
            var leaveTypes = await _client.LeaveTypeAllAsync();
            return _mapper.Map<List<LeaveTypeVM>>(leaveTypes);
        }

        public async Task<LeaveTypeVM> GetLeaveTypeDetailsAsync(int id)
        {
            await AddBearerToken();
            var leaveType = await _client.LeaveTypeGETAsync(id);
            return _mapper.Map<LeaveTypeVM>(leaveType);
        }

        public async Task<Response<Guid>> CreateLeaveTypeAsync(LeaveTypeVM leaveType)
        {
            try
            {
                await AddBearerToken();
                var createLeaveTypeCommand = _mapper.Map<CreateLeaveTypeCommand>(leaveType);
                await _client.LeaveTypePOSTAsync(createLeaveTypeCommand);
                return new Response<Guid>()
                {
                    Success = true
                };
            }
            catch (ApiException e)
            {
                return ConvertApiExceptions<Guid>(e);
            }
        }

        public async Task<Response<Guid>> UpdateLeaveTypeAsync(int id, LeaveTypeVM leaveType)
        {
            try
            {
                await AddBearerToken();
                var updateLeaveTypeCommand = _mapper.Map<UpdateLeaveTypeCommand>(leaveType);
                await _client.LeaveTypePUTAsync(id.ToString(), updateLeaveTypeCommand);
                return new Response<Guid>()
                {
                    Success = true
                };
            }
            catch (ApiException e)
            {
                return ConvertApiExceptions<Guid>(e);
            }
        }

        public async Task<Response<Guid>> DeleteLeaveTypeAsync(int id)
        {
            try
            {
                await AddBearerToken();
                await _client.LeaveTypeDELETEAsync(id);
                return new Response<Guid>()
                {
                    Success = true
                };
            }
            catch (ApiException e)
            {
                return ConvertApiExceptions<Guid>(e);
            }
        }
    }
}
