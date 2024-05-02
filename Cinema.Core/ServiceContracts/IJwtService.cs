﻿using Cinema.Core.Domain.IdentityEntities;
using Cinema.Core.DTO;

namespace Cinema.Core.ServiceContracts
{
    public interface IJwtService
    {
        AuthenticationResponse CreateJwtToken(ApplicationUser user);
    }
}
