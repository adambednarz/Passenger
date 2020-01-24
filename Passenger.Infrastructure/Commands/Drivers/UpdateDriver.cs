using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Infrastructure.Commands.Drivers
{
    public class UpdateDriver : AuthenticatedCommandBase
    {
        public string VehicleBrand { get; set; }
        public string VehicleName { get; set; }
    }
}
