﻿using System;
using System.Collections.Generic;

namespace Cinema.Infrastructure.DatabaseContext;

public partial class Category
{
    public Guid Id { get; set; }

    public string? Name { get; set; }
}
