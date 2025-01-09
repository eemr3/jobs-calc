using System.ComponentModel.DataAnnotations;
using JobsCalc.Api.Application.Validators;

namespace JobsCalc.Api.Domain.Entities;

public class User
{
  public int UserId { get; set; }
  [Required]
  [StringLength(100, MinimumLength = 3)]
  public string FullName { get; set; } = null!;
  [Required]
  public string Email { get; set; } = null!;
  [Required]
  public string PasswordHash { get; set; } = null!;
  public string? AvatarUrl { get; set; }
  public DateTime CreatedAt { get; set; }
  public Planning? Planning { get; set; }
  public ICollection<Job>? Jobs { get; set; }
}