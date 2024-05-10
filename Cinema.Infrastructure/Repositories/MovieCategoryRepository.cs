using Cinema.Core.Domain.Entities;
using Cinema.Core.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repositories;

public class MovieCategoryRepository : Repository<MovieCategory>, IMovieCategoryRepository
{
    public MovieCategoryRepository(SqlcinemadbContext dataContext) : base(dataContext)
    {
        
    }

    protected override IQueryable<MovieCategory> PrepareDbSet()
    {
        return base.PrepareDbSet()
            .Include(x => x.Category)
            .Include(x => x.Movie);
    }
}