﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Infrastructure.DTO
{
    public class JwtDto
    {
        public string Token { get; set; }
        public long Expiries { get; set; }
    }
}
