using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Infrastructure.Extensions
{
    public static class DateTimeExtensions
    {
        public static long ToTimeStamp(this DateTime dateTime)
            => ((DateTimeOffset)dateTime).ToUnixTimeSeconds();
    }
}
