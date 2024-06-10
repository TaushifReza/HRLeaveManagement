using MediatR;

namespace Application.Features.LeaveType.Queries.GetLeaveTypeDetails
{
    public record GetLeaveTypeDetailsQuery(int Id) : IRequest<LeaveTypeDetailDto>;
}
