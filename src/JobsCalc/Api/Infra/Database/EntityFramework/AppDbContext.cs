using JobsCalc.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobsCalc.Api.Infra.Database.EntityFramework;

public class AppDbContext : DbContext, IAppDbContext
{

  public DbSet<User> Users { get; set; } = null!;
  public DbSet<Planning> Plannings { get; set; } = null!;
  public DbSet<Job> Jobs { get; set; } = null!;

  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<User>(entity =>
    {
      entity.ToTable("users");
      entity.HasKey(usr => usr.UserId);
      entity.HasIndex(usr => usr.Email).IsUnique();
      entity.Property(usr => usr.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

      entity.HasOne(usr => usr.Planning)
          .WithOne(pl => pl.User).HasForeignKey<Planning>(pl => pl.UserId);

      entity.HasMany(usr => usr.Jobs)
          .WithOne(job => job.User).HasForeignKey(job => job.UserId)
          .OnDelete(DeleteBehavior.Cascade);
    });


    modelBuilder.Entity<Planning>(entity =>
    {
      entity.ToTable("plannings");
      entity.HasKey(pl => pl.PlanningId);
      entity.Property(pl => pl.MonthlyBudget).HasColumnType("numeric(10,2)");
      entity.Property(pl => pl.ValueHour).HasColumnType("numeric(10,2)");
    });

    modelBuilder.Entity<Job>(entity =>
    {
      entity.ToTable("jobs");
      entity.HasKey(job => job.JobId);
      entity.Property(job => job.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
    });

    // Iterar sobre as entidades
    foreach (var entityType in modelBuilder.Model.GetEntityTypes())
    {
      // Iterar sobre as propriedades da entidade
      foreach (var property in entityType.GetProperties())
      {
        // Converter o nome da propriedade para snake_case
        var snakeCaseName = ConvertToSnakeCase(property.Name);

        // Alterar o nome da coluna para snake_case
        property.SetColumnName(snakeCaseName);
      }
    }

    base.OnModelCreating(modelBuilder);
  }

  public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    return base.SaveChangesAsync(cancellationToken);
  }

  /// <summary>
  /// Converte um nome de string para snake_case.
  /// </summary>
  private static string ConvertToSnakeCase(string input)
  {
    return string.Concat(
        input.Select((ch, index) =>
            index > 0 && char.IsUpper(ch)
                ? "_" + char.ToLower(ch)
                : char.ToLower(ch).ToString())
    );
  }
}