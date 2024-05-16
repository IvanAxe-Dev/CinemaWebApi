using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Core.Domain.Entities;

namespace Cinema.Core.ServiceContracts
{
    public interface IUserMovieRateService : IService<UserMovieRate>
    {
        Task<int?> GetUserMovieRating(Guid userId);
    }
}
