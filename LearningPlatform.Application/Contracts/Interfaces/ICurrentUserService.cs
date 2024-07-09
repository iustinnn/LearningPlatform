using System.Security.Claims;

namespace LearningPlatform.Application.Contracts.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        ClaimsPrincipal GetCurrentClaimsPrincipal();
        string GetCurrentUserId();

        string GetCurrentUserName();
    }
}
