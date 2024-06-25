using Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using AutoMapper;
using Domain.EntityModels;

namespace Application.MappingProfiles
{
    public class LeaveAllocationProfile : Profile
    {
        public LeaveAllocationProfile()
        {
            CreateMap<LeaveAllocationDto, LeaveAllocation>().ReverseMap();
            CreateMap<LeaveAllocationDetailsDto, LeaveAllocation>().ReverseMap();
            CreateMap<CreateLeaveAllocationCommand, LeaveAllocation>().ReverseMap();
            CreateMap<UpdateLeaveAllocationCommand, LeaveAllocation>().ReverseMap();
        }
    }
}
