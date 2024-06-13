using Application.Contract.Persistence;
using Domain.EntityModels;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContext;
using Repositorys;

namespace Persistence.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(HrDatabaseContext context) : base(context)
        {
        }

        public async Task<bool> IsLeaveTypeNameUnique(string name)
        {
            return await _context.LeaveTypes.AnyAsync(q => string.Equals(q.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
