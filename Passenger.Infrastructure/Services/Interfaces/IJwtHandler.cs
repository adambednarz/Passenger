using Passenger.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Infrastructure.Services.Interfaces
{
    public interface IJwtHandler
    {
        JwtDto CreateToken(string email, string role);
    }
}
