using Hr.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Hr.LeaveManagement.BlazorUI.Services.Base;

namespace Hr.LeaveManagement.BlazorUI.Contracts
{
    public interface ILeaveTypeService
    {
        Task<List<LeaveTypeVM>> GetLeaveTypesAsync();
        Task<LeaveTypeVM> GetLeaveTypeDetailsAsync(int id);
        Task<Response<Guid>> CreateLeaveTypeAsync(LeaveTypeVM leaveType);
        Task<Response<Guid>> UpdateLeaveTypeAsync(int id, LeaveTypeVM leaveType);
        Task<Response<Guid>> DeleteLeaveTypeAsync(int id);
    }
}
