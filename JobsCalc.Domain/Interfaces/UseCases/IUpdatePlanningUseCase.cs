using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Domain.Entities;

namespace JobsCalc.Domain.Interfaces.UseCases;

public interface IUpdatePlanningUseCase
{
  public Task<PlanningEntity> Execute(PlanningUpdateRequest request);
}