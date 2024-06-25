using Application.Features.LeaveRequest.Shared;
using MediatR;

namespace Application.Features.LeaveRequest.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestCommand : BaseLeaveRequest, IRequest<Unit>
    {
        public int Id { get; set; }
        public string RequestComments { get; set; }
        public bool Cancelled { get; set; }
    }
}
