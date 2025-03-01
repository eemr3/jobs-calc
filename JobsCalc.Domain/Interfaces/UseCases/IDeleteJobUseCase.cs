namespace JobsCalc.Domain.Interfaces.UseCases;

public interface IDeleteJobUseCase
{
  public Task Execute(string jobId);
}