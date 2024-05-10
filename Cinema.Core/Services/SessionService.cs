﻿using Cinema.Core.Domain.Entities;
using Cinema.Core.Domain.RepositoryContracts;
using Cinema.Core.ServiceContracts;
using MapsterMapper;

namespace Cinema.Core.Services
{
    public class SessionService : Service<Session>, ISessionService
    {
        private readonly ISessionRepository _repository;
        private readonly IMapper _mapster;

        public SessionService(ISessionRepository repository, IMapper mapster) : base(repository)
        {
            _repository = repository;
            _mapster = mapster;
        }
    }
}