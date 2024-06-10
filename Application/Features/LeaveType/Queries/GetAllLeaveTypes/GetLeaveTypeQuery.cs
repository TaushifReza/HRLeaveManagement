using MediatR;

namespace Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
    public record GetLeaveTypeQuery : IRequest<List<LeaveTypeDto>>;
}
