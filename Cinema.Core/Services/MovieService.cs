﻿using Cinema.Core.Domain.Entities;
using Cinema.Core.Domain.RepositoryContracts;
using Cinema.Core.ServiceContracts;
using MapsterMapper;

namespace Cinema.Core.Services
{
    public class MovieService : Service<Movie>, IMovieService
    {
        private readonly IMapper _mapster;
        private readonly IMovieRepository _repository;

        public MovieService(IMovieRepository repository, IMapper mapster) : base(repository)
        {
            _mapster = mapster;
            _repository = repository;
        }
    }
}