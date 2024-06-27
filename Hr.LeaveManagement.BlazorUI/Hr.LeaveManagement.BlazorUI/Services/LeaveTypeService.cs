using AutoMapper;
using Hr.LeaveManagement.BlazorUI.Contracts;
using Hr.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Hr.LeaveManagement.BlazorUI.Services.Base;

namespace Hr.LeaveManagement.BlazorUI.Services
{
    public class LeaveTypeService : BaseHttpService, ILeaveTypeService
    {
        private readonly IMapper _mapper;

        public LeaveTypeService(IClient client, IMapper mapper) : base(client)
        {
            _mapper = mapper;
        }

        public async Task<List<LeaveTypeVM>> GetLeaveTypesAsync()
        {
            var leaveTypes = await _client.LeaveTypeAllAsync();
            return _mapper.Map<List<LeaveTypeVM>>(leaveTypes);
        }

        public async Task<LeaveTypeVM> GetLeaveTypeDetailsAsync(int id)
        {
            var leaveType = await _client.LeaveTypeGETAsync(id);
            return _mapper.Map<LeaveTypeVM>(leaveType);
        }

        public async Task<Response<Guid>> CreateLeaveTypeAsync(LeaveTypeVM leaveType)
        {
            try
            {
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
