using Application.Contract.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public UpdateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            // Validate the incoming data

            // convert to domain entity object
            var leaveTypeToUpdate = _mapper.Map<Domain.EntityModels.LeaveType>(request);

            // save to database
            await _leaveTypeRepository.UpdateAsync(leaveTypeToUpdate);

            // return the id of the new record
            return Unit.Value;
        }
    }
}
