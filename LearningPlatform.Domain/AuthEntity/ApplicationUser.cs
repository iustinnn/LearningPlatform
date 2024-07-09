using Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Domain.AuthEntity;

public class ApplicationUser
{
    [Key] public Guid UserId { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string Username { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
}