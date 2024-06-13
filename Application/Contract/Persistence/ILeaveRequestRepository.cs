using Domain.EntityModels;

namespace Application.Contract.Persistence;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
    Task<LeaveRequest?> GetLeaveRequestWithDetailsAsync(int id);
    Task<List<LeaveRequest>> GetLeaveRequestWithDetailsAsync();
    Task<List<LeaveRequest>> GetLeaveRequestWithDetailsAsync(string userId);
}