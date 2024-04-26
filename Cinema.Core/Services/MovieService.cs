using Cinema.Core.Domain.RepositoryContracts;
using Cinema.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Services
{
    public class MovieService
    {
        private readonly IRepository<Movie> _movieRepository;

        public MovieService(IRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<List<Movie>> GetAllMoviesAsync() 
        {
            return await _movieRepository.GetAll().ToListAsync();
        }

        public async Task<Movie?> GetMovieByIdAsync(Guid id)
        {
            return await _movieRepository.GetFirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddMovieAsync(Movie movie)
        {
            await _movieRepository.Post(movie);
            //await _movieRepository.SaveChangesAsync();
        } 

        public void UpdateMovie(Movie movie)
        {
            _movieRepository.Update(movie);
        }

        public void DeleteMovie(Movie movie)
        {
            _movieRepository.Delete(movie);
        }
    }
}
