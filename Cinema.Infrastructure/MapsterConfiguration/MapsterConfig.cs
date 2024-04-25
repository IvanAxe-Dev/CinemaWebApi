using Cinema.Core.DTO;
using Cinema.Infrastructure.DatabaseContext;
using Mapster;

namespace Cinema.Infrastructure.MapsterConfiguration;

public static class MapsterConfig
{
    public static void Configure()
    {
        // Configure your AutoMapper here
        TypeAdapterConfig<Movie, MovieDto>.NewConfig()
            .Map(dest => dest.Title, src => src.Title)
            
            .Map(dest => dest.Description, src => src.Description)
            
            .Map(dest => dest.Image, src => src.Image)
            
            .Map(dest => dest.ReleaseDate, src => src.ReleaseDate)
            
            .Map(dest => dest.Director, src => src.Director)
            
            .Map(dest => dest.Duration, src => src.Duration)
            
            .Map(dest => dest.TrailerUrl, src => src.TrailerUrl)
            
            .Map(dest => dest.Actors, src => src.Actors)
            
            .Map(dest => dest.Rating, src => src.Rating)
            
            .Map(dest => dest.Sessions, src => src.Sessions);
    }
}