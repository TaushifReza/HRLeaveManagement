using Application.Contract.Email;
using Application.Contract.Logging;
using Application.Contract.Persistence;
using Application.Exceptions;
using Application.Models.Email;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveRequest.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Unit>
    {
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IAppLogger<CreateLeaveRequestCommandHandler> _appLogger;

        public CreateLeaveRequestCommandHandler(IEmailSender emailSender, IMapper mapper, ILeaveTypeRepository leaveTypeRepository, ILeaveRequestRepository leaveRequestRepository, IAppLogger<CreateLeaveRequestCommandHandler> appLogger)
        {
            _emailSender = emailSender;
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
            _leaveRequestRepository = leaveRequestRepository;
            _appLogger = appLogger;
        }
        public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Leave Request", validationResult);

            // Get requesting employee id

            // check on employee allocation 

            // if allocation aren't enough, return validation error with message

            // create leave request
            var leaveRequest = _mapper.Map<Domain.EntityModels.LeaveRequest>(request);
            await _leaveRequestRepository.CreateAsync(leaveRequest);

            // send confirmation email
            try
            {
                var email = new EmailMessage
                {
                    To = string.Empty, /* Get Email from employee record */
                    Body =
                        $"Your leave Request for {request.StartDate:D} to {request.EndDate:D} has been submitted successful",
                    Subject = "Leave Request Submitted"
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
