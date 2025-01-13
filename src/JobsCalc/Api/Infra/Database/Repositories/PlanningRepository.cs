using JobsCalc.Api.Domain.Entities;
using JobsCalc.Api.Infra.Database.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace JobsCalc.Api.Infra.Database.Repositories;

public class PlanningRepository : IPlanningRepository
{
  private readonly IAppDbContext _context;

  public PlanningRepository(IAppDbContext context)
  {
    _context = context;
  }

  public async Task<Planning> AddPlanningAsync(Planning planning)
  {
    var planningAdd = await _context.Plannings.AddAsync(planning);

    await _context.SaveChangesAsync();

    return planningAdd.Entity;
  }

  public async Task<Planning?> GetPlanningByUserAsync(int userId)
  {
    var planning = await _context.Plannings
                      .Where(pl => pl.UserId.Equals(userId))
                      .FirstOrDefaultAsync();
    if (planning is null) return null;

    return planning;
  }

  public async Task<Planning?> UpdatePlanningAsync(Planning planning)
  {
    var planinigExists = await _context.Plannings.FindAsync(planning.PlanningId);

    if (planinigExists is null) return null;

    _context.Entry(planinigExists).State = EntityState.Detached;

    _context.Plannings.Update(planning);
    await _context.SaveChangesAsync();

    return planning;
  }
}