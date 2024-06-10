using Domain.EntityModels;

namespace Application.Contract.Persistence;

public interface ILeaveTypeRepository : IGenericRepository<LeaveType>
{
    Task<bool> IsLeaveTypeNameUnique(string name);
}