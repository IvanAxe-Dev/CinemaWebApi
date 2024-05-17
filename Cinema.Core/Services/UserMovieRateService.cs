using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Core.Domain.Entities;
using Cinema.Core.Domain.RepositoryContracts;
using Cinema.Core.ServiceContracts;
using MapsterMapper;

namespace Cinema.Core.Services
{
    public class UserMovieRateService : Service<UserMovieRate>, IUserMovieRateService
    {
        private readonly IUserMovieRateRepository _repository;
        private readonly IMapper _mapster;

        public UserMovieRateService(IUserMovieRateRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<int?> GetUserMovieRating(Guid userId)
        {
            UserMovieRate? userMovieRate = await _repository.GetFirstOrDefaultAsync(u => u.ApplicationUserId == userId);

            if (userMovieRate == null)
            {
                return null;
            }

            return userMovieRate.Rating;
        }
    }
}
