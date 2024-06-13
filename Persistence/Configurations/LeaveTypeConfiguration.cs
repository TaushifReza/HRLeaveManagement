using Domain.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.HasData(new LeaveType
            {
                Id = 1,
                Name = "Vacation",
                DefaultDays = 7,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            });
        }
    }
}
