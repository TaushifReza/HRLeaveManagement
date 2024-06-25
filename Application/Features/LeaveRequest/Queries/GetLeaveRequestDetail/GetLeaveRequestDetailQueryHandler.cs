using Application.Contract.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail
{
    public class GetLeaveRequestDetailQueryHandler : IRequestHandler<GetLeaveRequestDetailQuery, LeaveRequestDetailsDto>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;

        public GetLeaveRequestDetailQueryHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
        }
        public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailQuery request, CancellationToken cancellationToken)
        {
            var leaveRequest =
                _mapper.Map<LeaveRequestDetailsDto>(
                    await _leaveRequestRepository.GetLeaveRequestWithDetailsAsync(request.Id));
            // Add Employee Details as needed

            return leaveRequest;
        }
    }
}
