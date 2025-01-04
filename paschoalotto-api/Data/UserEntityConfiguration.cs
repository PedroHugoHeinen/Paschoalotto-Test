using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using paschoalotto_api.Models;

namespace paschoalotto_api.Data
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(usr => usr.Status)
           .HasDefaultValue(true);
        }
    }
}
