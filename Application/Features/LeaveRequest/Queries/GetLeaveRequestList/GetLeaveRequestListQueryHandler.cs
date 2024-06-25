using Application.Contract.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveRequest.Queries.GetLeaveRequestList
{
    public class GetLeaveRequestListQueryHandler : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDto>>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;

        public GetLeaveRequestListQueryHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
        }
        public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
        {
            // Checked if it is logged in employee

            var leaveRequests = await _leaveRequestRepository.GetLeaveRequestWithDetailsAsync();
            var requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);

            // Fill request with employee
            return requests;
        }
    }
}
