using Application.Contract.Persistence;
using Application.Exceptions;
using Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveType.Queries.GetLeaveTypeDetails
{
    public class GetLeaveTypeDetailsQueryHandler : IRequestHandler<GetLeaveTypeDetailsQuery, LeaveTypeDetailDto>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public GetLeaveTypeDetailsQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<LeaveTypeDetailDto> Handle(GetLeaveTypeDetailsQuery request, CancellationToken cancellationToken)
        {
            // Query the database
            var leaveType = await _leaveTypeRepository.GetByIdAsync(request.Id);

            // Validate if the record exists
            if (leaveType == null)
                throw new NotFoundException(nameof(LeaveType), request.Id);

            // Map the results to LeaveTypeDto
            var data = _mapper.Map<LeaveTypeDetailDto>(leaveType);

            // Return the list of LeaveTypeDto
            return data;
        }
    }
}
