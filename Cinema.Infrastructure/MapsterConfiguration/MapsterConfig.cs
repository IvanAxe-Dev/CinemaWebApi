﻿using Cinema.Core.DTO;
using Cinema.Infrastructure.DatabaseContext;
using Mapster;

namespace Cinema.Infrastructure.MapsterConfiguration;

public static class MapsterConfig
{
    public static void Configure()
    {

        TypeAdapterConfig<Movie, MovieDto>.NewConfig()
            .Map(dest => dest.Title, src => src.Title)
            
            .Map(dest => dest.Description, src => src.Description)
            
            .Map(dest => dest.Image, src => src.Image)
            
            .Map(dest => dest.ReleaseDate, src => src.ReleaseDate.Value.ToString("dd.MM.yyyy"))
            
            .Map(dest => dest.Director, src => src.Director)
            
            .Map(dest => dest.Duration, src => src.Duration.Value.ToString("H 'hours' m 'minutes'"))
            
            .Map(dest => dest.TrailerUrl, src => src.TrailerUrl)
            
            .Map(dest => dest.Actors, src => src.Actors)
            
            .Map(dest => dest.Rating, src => src.Rating)
            
            .Map(dest => dest.Sessions, src => src.Sessions);

        TypeAdapterConfig<Category, CategoryDto>.NewConfig()
            .Map(dest => dest.Name, src => src.Name);
    }
}