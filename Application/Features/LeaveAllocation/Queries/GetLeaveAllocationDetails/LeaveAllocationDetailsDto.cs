﻿using Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails
{
    public class LeaveAllocationDetailsDto
    {
        public int Id { get; set; }
        public int NumberOfDays { get; set; }
        public LeaveTypeDto LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
    }
}
