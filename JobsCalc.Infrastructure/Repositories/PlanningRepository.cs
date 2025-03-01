using JobsCalc.Domain.Entities;
using JobsCalc.Domain.Interfaces.Repositories;
using JobsCalc.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JobsCalc.Infrastructure.Repositories;

public class PlanningRepository : IPlanningRepository
{
  private readonly IDbContextFactory<ApiDbContext> _contextFactory;

  public PlanningRepository(IDbContextFactory<ApiDbContext> contextFactory)
  {
    _contextFactory = contextFactory;
  }

  public async Task<PlanningEntity> CreatePlanningAsync(PlanningEntity planningEntity)
  {
    await using var context = await _contextFactory.CreateDbContextAsync();

    await context.Plannings.AddAsync(planningEntity);
    await context.SaveChangesAsync();

    return planningEntity;
  }

  public async Task<PlanningEntity?> GetPlanningByUserIdAsync(int userId)
  {
    await using var context = await _contextFactory.CreateDbContextAsync();

    return await context.Plannings.FirstOrDefaultAsync(planning => planning.UserId.Equals(userId));
  }

  public async Task<PlanningEntity> UpdatePlanningAsync(PlanningEntity planningEntity)
  {
    await using var context = await _contextFactory.CreateDbContextAsync();

    context.Entry(planningEntity).State = EntityState.Detached;
    await context.SaveChangesAsync();

    return planningEntity;
  }
}