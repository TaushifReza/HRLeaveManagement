using Application.Contract.Logging;
using Application.Contract.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
    public class GetLeaveTypeQueryHandler : IRequestHandler<GetLeaveTypeQuery, List<LeaveTypeDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IAppLogger<GetLeaveTypeQueryHandler> _logger;

        public GetLeaveTypeQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, IAppLogger<GetLeaveTypeQueryHandler> logger)
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
            _logger = logger;
        }

        public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypeQuery request, CancellationToken cancellationToken)
        {
            // Query the database
            var leaveTypes = await _leaveTypeRepository.GetAllAsync();

            // Map the results to LeaveTypeDto
            var data = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

            // Return the list of LeaveTypeDto
            _logger.LogInformation("Leave Type were retrieved successful.");
            return  data;
        }
    }
}
