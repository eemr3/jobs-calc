namespace JobsCalc.Api.Application.Utils;
public class WorkHourCalculator
{
  private static readonly int weeksPerYear = 52;
  public static decimal CalculateHourlyRate(decimal monthlyBudget, int vacationPerYear, int hoursPerDay, int daysPerWeek)
  {
    // Calcula a quantidade de semanas por mês
    var weeksPerMonth = (weeksPerYear - vacationPerYear) / 12m;

    // Calcula a quantidade total de horas por semana
    var weekTotalHours = hoursPerDay * daysPerWeek;

    // Calcula a quantidade total de horas por mês
    var monthlyTotalHours = weekTotalHours * weeksPerMonth;

    // Calcula o valor da hora
    var hourlyRate = monthlyBudget / monthlyTotalHours;

    return decimal.Round(hourlyRate, 2); // Arredonda o valor para 2 casas decimais
  }
}

