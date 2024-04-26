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
    public class SessionService
    {
        private readonly IRepository<Session> _sessionRepository;

        public SessionService(IRepository<Session> sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<List<Session>> GetAllSessionsAsync()
        {
            return await _sessionRepository.GetAll().ToListAsync();
        }

        public async Task<Session?> GetSessionByIdAsync(Guid id)
        {
            return await _sessionRepository.GetFirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddSessionAsync(Session session)
        {
            await _sessionRepository.Post(session);
            //await _sessionRepository.SaveChangesAsync();
        }

        public void UpdateSession(Session session)
        {
            _sessionRepository.Update(session);
        }

        public void DeleteSession(Session session)
        {
            _sessionRepository.Delete(session);
        }
        
    }
}
