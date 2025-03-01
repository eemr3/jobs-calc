using JobsCalc.Domain.Entities;

namespace JobsCalc.Domain.Services;

public class RemainingDaysCalculator
{
  public static int RemainingDays(JobEntity job, int dailyHours, int totalHours)
  {
    var remainingDays = totalHours / dailyHours;
    var dueDate = job.CreatedAt.AddDays(remainingDays);

    var timeDiff = dueDate - DateTime.Now;

    var dayDiff = (int)Math.Floor(timeDiff.TotalDays);

    return dayDiff;
  }
}