﻿namespace Cinema.Core.Domain.Entities
{
    public partial class MovieCategory : BaseEntity
    {
        public Guid CategoryId { get; set; }

        public Guid MovieId { get; set; }

        public Category Category { get; set; }

        public Movie Movie { get; set; }
    }
}