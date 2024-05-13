using Cinema.Core.DTO;
using Cinema.Core.Domain.Entities;
using Mapster;

namespace Cinema.Infrastructure.MapsterConfiguration;

public static class MapsterConfig
{
    public static void Configure()
    {

        TypeAdapterConfig<Movie, MovieDto>.NewConfig()
            .Map(dest => dest.Title, src => src.Title)

            .Map(dest => dest.Description, src => src.Description)

            .Map(dest => dest.ImageUrl, src => src.ImageUrl)

            .Map(dest => dest.ReleaseDate, src => src.ReleaseDate.Value.ToString("dd.MM.yyyy"))

            .Map(dest => dest.Director, src => src.Director)

            .Map(dest => dest.Duration, src => src.Duration)

            .Map(dest => dest.TrailerUrl, src => src.TrailerUrl)

            .Map(dest => dest.Actors, src => src.Actors);

        TypeAdapterConfig<Category, CategoryDto>.NewConfig()
            .Map(dest => dest.Name, src => src.Name);

        TypeAdapterConfig<Session, SessionResponse>.NewConfig()
            .Map(dest => dest.CinemaHallId, src => src.CinemaHallId)

            .Map(dest => dest.AvailableSeats, src => src.AvailableSeats)

            .Map(dest => dest.MovieId, src => src.MovieId)

            .Map(dest => dest.StartTime, src => TimeOnly.FromDateTime(src.StartTime).ToShortTimeString())

            .Map(dest => dest.Date, src => DateOnly.FromDateTime(src.Date))

            .Map(dest => dest.Id, src => src.Id)

            .Map(dest => dest.Price, src => src.Price)

            .Map(dest => dest.Seats, src => src.Seats);
    }
}