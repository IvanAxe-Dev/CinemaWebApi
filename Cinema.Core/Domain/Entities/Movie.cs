﻿namespace Cinema.Core.Domain.Entities;

public partial class Movie : BaseEntity
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public byte[]? Image { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public string? Director { get; set; }

    public TimeOnly? Duration { get; set; }

    public string? TrailerUrl { get; set; }

    public string? Actors { get; set; }

    public double? Rating { get; set; }

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
