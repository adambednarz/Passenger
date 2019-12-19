using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services
{
    public class JwtHandler : IJwtHandler
    {
        public JwtDto CreateToken(Guid userId, string role)
        {
            throw new NotImplementedException();
        }

    }
}
