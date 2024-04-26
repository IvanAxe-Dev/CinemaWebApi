using System.ComponentModel.DataAnnotations;

namespace Cinema.Core.Domain.Entities
{
    public class BaseEntity : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
