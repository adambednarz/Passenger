using System;

namespace Passenger.Infrastructure.Commands.Drivers
{
    public class CreateDriver : AuthenticatedCommandBase
    {
        public  string VehicleBrand{ get; set; }
        public  string VehicleName{ get; set; }
    }
}