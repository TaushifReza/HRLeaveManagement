using Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using AutoMapper;
using Domain.EntityModels;

namespace Application.MappingProfiles
{
    public class LeaveTypeProfile : Profile
    {
        public LeaveTypeProfile()
        {
            CreateMap<LeaveTypeDto, LeaveType>().ReverseMap();
            CreateMap<LeaveTypeDetailDto, LeaveType>().ReverseMap();
        }
    }
}
