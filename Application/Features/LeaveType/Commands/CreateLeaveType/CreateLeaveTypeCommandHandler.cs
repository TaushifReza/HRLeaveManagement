using Application.Contract.Persistence;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            // Validate the incoming data
            var validator = new CreateLeaveTypeCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid LeaveType", validationResult);

            // convert to domain entity object
            var leaveTypeToCreate = _mapper.Map<Domain.EntityModels.LeaveType>(request);

            // save to database
            await _leaveTypeRepository.CreateAsync(leaveTypeToCreate);

            // return the id of the new record
            return leaveTypeToCreate.Id;
        }
    }
}
