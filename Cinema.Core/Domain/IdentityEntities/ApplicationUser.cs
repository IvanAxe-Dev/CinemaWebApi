using Microsoft.AspNetCore.Identity;
using System.Data;
using Cinema.Core.Domain.Entities;
using Cinema.Core.Enums;

namespace Cinema.Core.Domain.IdentityEntities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
        public UserRoleOptions Role { get; set; }
        public List<Ticket> UserTickets { get; set; }
        
        public List<string>? RecentlyWatchedCategories { get; set; }
    }
}
