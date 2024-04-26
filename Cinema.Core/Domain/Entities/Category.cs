using Cinema.Core.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Cinema.Infrastructure.DatabaseContext;

public partial class Category : BaseEntity
{
    public string? Name { get; set; }
}
