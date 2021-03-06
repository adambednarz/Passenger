﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Infrastructure.Services.Interfaces
{
    public interface IEncrypter
    {
        string GetSalt(string value);
        string GetHash(string value, string salt);
    }
}
