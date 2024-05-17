using Cinema.Core.Domain.IdentityEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Core.Domain.Entities
{
    public class UserMovieRate : BaseEntity
    {
        public Guid MovieId { get; set; }
        public Guid ApplicationUserId { get; set; }
        public Movie Movie { get; set; }
        public int? Rating { get; set; }
    }
}
