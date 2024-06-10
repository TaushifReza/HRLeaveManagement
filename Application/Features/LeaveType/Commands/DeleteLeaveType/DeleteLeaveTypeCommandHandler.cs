using Application.Contract.Persistence;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveType.Commands.DeleteLeaveType
{
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            // retrieve domain entity object
            var leaveTypeToDelete = await _leaveTypeRepository.GetByIdAsync(request.Id);

            // validate if the record exists
            if (leaveTypeToDelete == null)
                throw new NotFoundException(nameof(LeaveType), request.Id);

            // delete the record
            await _leaveTypeRepository.DeleteAsync(leaveTypeToDelete);

            // return the id of the new record
            return Unit.Value;
        }
    }
}
