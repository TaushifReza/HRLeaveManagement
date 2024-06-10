using MediatR;

namespace Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommand : IRequest<Unit>
    {
        public string Name { get; set; } = string.Empty;
        public int DefaultDays { get; set; }
    }
}
