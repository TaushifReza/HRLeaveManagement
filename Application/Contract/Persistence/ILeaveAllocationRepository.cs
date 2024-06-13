using Domain.EntityModels;

namespace Application.Contract.Persistence;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
{
    Task<LeaveAllocation?> GetLeaveAllocationWithDetailsAsync(int id);
    Task<List<LeaveAllocation>> GetLeaveAllocationWithDetailsAsync();
    Task<List<LeaveAllocation>> GetLeaveAllocationWithDetailsAsync(string userId);
    Task<bool> AllocationExistsAsync(string userId, int leaveTypeId, int period);
    Task AddAllocationsAsync(List<LeaveAllocation> allocations);
    Task<LeaveAllocation?> GetUserAllocationsAsync(string userId, int leaveTypeId);
}