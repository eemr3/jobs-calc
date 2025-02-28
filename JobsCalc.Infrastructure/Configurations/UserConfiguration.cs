using JobsCalc.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobsCalc.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("users");
        builder.HasKey(usr => usr.UserId);
        builder.Property(usr => usr.FullName).IsRequired().HasColumnType("varchar(150)");
        builder.HasIndex(usr => usr.Email).IsUnique();
        builder.Property(usr => usr.Email).IsRequired().HasColumnType("varchar(150)");
        builder.Property(usr => usr.PasswordHash).IsRequired().HasColumnType("varchar(64)");
        builder.Property(usr => usr.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(usr => usr.Planning)
                .WithOne(pl => pl.User).HasForeignKey<PlanningEntity>(pl => pl.UserId);

        builder.HasMany(usr => usr.Jobs)
                .WithOne(job => job.User).HasForeignKey(job => job.UserId)
                .OnDelete(DeleteBehavior.Cascade);
    }
}   