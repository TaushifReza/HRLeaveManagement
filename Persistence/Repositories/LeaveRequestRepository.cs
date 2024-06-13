using Application.Contract.Persistence;
using Domain.EntityModels;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContext;
using Repositorys;
using System.Linq;

namespace Persistence.Repositories;

public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
    public LeaveRequestRepository(HrDatabaseContext context) : base(context)
    {
    }

    public async Task<LeaveRequest?> GetLeaveRequestWithDetailsAsync(int id)
    {
        var leaveRequest = await _context.LeaveRequests
            .Include(q => q.LeaveType)
            .FirstOrDefaultAsync(q => q.Id == id);
        return leaveRequest;
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestWithDetailsAsync()
    {
        var leaveRequest = await _context.LeaveRequests
            .Include(q => q.LeaveType)
            .ToListAsync();
        return leaveRequest;
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestWithDetailsAsync(string userId)
    {
        var leaveRequest = await _context.LeaveRequests
            .Where(q => q.RequestingEmployee == userId)
            .Include(q => q.LeaveType)
            .ToListAsync();
        return leaveRequest;
    }
}