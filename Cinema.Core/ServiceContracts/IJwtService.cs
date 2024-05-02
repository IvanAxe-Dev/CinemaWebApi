using System.Security.Claims;
using Cinema.Core.Domain.IdentityEntities;
using Cinema.Core.DTO;

namespace Cinema.Core.ServiceContracts
{
    public interface IJwtService
    {
        AuthenticationResponse CreateJwtToken(ApplicationUser user);
        ClaimsPrincipal? GetPrincipalFromJwtToken(string? token);
    }
}
