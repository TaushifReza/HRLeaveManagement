using Application.Contract.Email;
using Application.Contract.Logging;
using Application.Contract.Persistence;
using Application.Exceptions;
using Application.Models.Email;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval
{
    public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IAppLogger<ChangeLeaveRequestApprovalCommandHandler> _appLogger;

        public ChangeLeaveRequestApprovalCommandHandler(IMapper mapper, IEmailSender emailSender, ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository, IAppLogger<ChangeLeaveRequestApprovalCommandHandler> appLogger)
        {
            _mapper = mapper;
            _emailSender = emailSender;
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _appLogger = appLogger;
        }
        public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

            if (leaveRequest is null)
                throw new NotFoundException(nameof(LeaveRequest), request.Id);

            // if request is approved, get and update the employee's allocation

            // send email confirmation
            try
            {
                var email = new EmailMessage
                {
                    To = string.Empty, /* Get Email from Employee record*/
                    Body = $"The Approval status for your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been Updated",
                    Subject = "Leave Request Approval Status Updated"
                };

                await _emailSender.SendEmailAsync(email);
            }
            catch (Exception e)
            {
                _appLogger.LogWarning(e.Message);
            }

            return Unit.Value;
        }
    }
}
