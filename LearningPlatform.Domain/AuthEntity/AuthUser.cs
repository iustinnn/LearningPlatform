using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;

namespace Domain.AuthEntity;

public class AuthUser : IdentityUser
{
    public string Email { get; set; }
    // public IdentityUser<string> UserName { get; set; }
    // public string Phone { get; set; }
}