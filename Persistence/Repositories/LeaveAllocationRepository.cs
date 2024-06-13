using Application.Contract.Persistence;
using Domain.EntityModels;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContext;
using Repositorys;

namespace Persistence.Repositories;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    public LeaveAllocationRepository(HrDatabaseContext context) : base(context)
    {
    }

    public async Task<LeaveAllocation?> GetLeaveAllocationWithDetailsAsync(int id)
    {
        var leaveAllocation = await _context.LeaveAllocations
            .Include(q => q.LeaveType)
            .FirstOrDefaultAsync(q => q.Id == id);
        return leaveAllocation;
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationWithDetailsAsync()
    {
        var leaveAllocations = await _context.LeaveAllocations
            .Include(q => q.LeaveType)
            .ToListAsync();
        return leaveAllocations;
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationWithDetailsAsync(string userId)
    {
        var leaveAllocations = await _context.LeaveAllocations
            .Where(q => q.EmployeeId == userId)
            .Include(q => q.LeaveType)
            .ToListAsync();
        return leaveAllocations;
    }

    public async Task<bool> AllocationExistsAsync(string userId, int leaveTypeId, int period)
    {
        return await _context.LeaveAllocations.AnyAsync(q => q.EmployeeId == userId
                                                             && q.LeaveTypeId == leaveTypeId 
                                                             && q.Period == period);
    }

    public async Task AddAllocationsAsync(List<LeaveAllocation> allocations)
    {
        await _context.AddRangeAsync(allocations);
        await _context.SaveChangesAsync();
    }

    public async Task<LeaveAllocation?> GetUserAllocationsAsync(string userId, int leaveTypeId)
    {
        return await _context.LeaveAllocations.FirstOrDefaultAsync(q =>
            q.EmployeeId == userId && q.LeaveTypeId == leaveTypeId);
    }
}