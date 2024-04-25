using System;
using System.Collections.Generic;

namespace Cinema.Infrastructure.DatabaseContext;

public partial class MovieCategory
{
    public Guid CategoryId { get; set; }

    public Guid MovieId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}
