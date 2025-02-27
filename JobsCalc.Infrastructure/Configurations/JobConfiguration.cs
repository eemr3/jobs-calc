using JobsCalc.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobsCalc.Infrastructure.Configurations;

public class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.ToTable("jobs");
        builder.HasKey(job => job.JobId);
        builder.Property(job => job.Name).HasColumnType("varchar(100)").IsRequired();
        builder.Property(job => job.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}