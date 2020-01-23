using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Infrastructure.Commands.Drivers
{
    public class CreateDriverRoute : AuthenticatedCommandBase
    {
        public string Name { get; set; }
        public double StartLatitude { get; set; }
        public double StartLongitude { get; set; }
        public double EndtLatitide { get; set; }
        public double EndtLongitude { get; set; }
    }
}
