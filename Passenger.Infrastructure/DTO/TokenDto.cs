using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Infrastructure.DTO
{
    public class TokenDto
    {
        public string Token { get; set; }
        public long Expires { get; set; }
        public string Role { get; set; }
    }
}
