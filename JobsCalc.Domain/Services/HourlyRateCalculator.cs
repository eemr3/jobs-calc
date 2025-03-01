namespace JobsCalc.Domain.Services;

public class HourlyRateCalculator
{
  private const int WeeksPerYear = 52;

  public static decimal CalculateHourlyRate(decimal monthlyBudget, int vacationPerYear, int hoursPerDay, int daysPerWeek)
  {
    // Calcula a quantidade de semanas por mês
    var weeksPerMonth = (WeeksPerYear - vacationPerYear) / 12m;

    // Calcula a quantidade total de horas por semana
    var weekTotalHours = hoursPerDay * daysPerWeek;

    // Calcula a quantidade total de horas por mês
    var monthlyTotalHours = weekTotalHours * weeksPerMonth;

    // Calcula o valor da hora
    var hourlyRate = monthlyBudget / monthlyTotalHours;

    return decimal.Round(hourlyRate, 2); // Arredonda o valor para 2 casas decimais
  }
}