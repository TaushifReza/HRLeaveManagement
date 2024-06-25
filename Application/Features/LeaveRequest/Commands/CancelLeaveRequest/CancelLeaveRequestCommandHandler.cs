using Application.Contract.Email;
using Application.Contract.Logging;
using Application.Contract.Persistence;
using Application.Exceptions;
using Application.Models.Email;
using MediatR;

namespace Application.Features.LeaveRequest.Commands.CancelLeaveRequest
{
    public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IEmailSender _emailSender;
        private readonly IAppLogger<CancelLeaveRequestCommandHandler> _appLogger;

        public CancelLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IEmailSender emailSender, IAppLogger<CancelLeaveRequestCommandHandler> appLogger)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _emailSender = emailSender;
            _appLogger = appLogger;
        }
        public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

            if (leaveRequest is null)
                throw new NotFoundException(nameof(leaveRequest), request.Id);

            leaveRequest.Cancelled = true;

            // if already approved, re-evaluate the employee's allocation for the leave type

            // Send email confirmation
            try
            {
                var email = new EmailMessage
                {
                    To = string.Empty, /* Get email from employee record */
                    Body = $"Your Leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been successful cancelled",
                    Subject = "Leave Request Cancelled"
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
