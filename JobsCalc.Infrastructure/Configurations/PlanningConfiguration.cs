using JobsCalc.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobsCalc.Infrastructure.Configurations;

public class PlanningConfiguration : IEntityTypeConfiguration<PlanningEntity>
{
    public void Configure(EntityTypeBuilder<PlanningEntity> builder)
    {
        builder.ToTable("plannings");
        builder.HasKey(pl => pl.PlanningId);
        builder.Property(pl => pl.MonthlyBudget).HasColumnType("numeric(10,2)");
        builder.Property(pl => pl.ValueHour).HasColumnType("numeric(10,2)");
    }
}