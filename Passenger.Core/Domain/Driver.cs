using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Core.Domain
{
    public class Driver
    {
        public Guid UserId { get; protected set; }
        public string Name { get; protected set; }
        public Vehicle Vehicle { get; protected set; }
        public IEnumerable<Route> Routes { get; protected set; }
        public IEnumerable<DailyRoute> DailyRoutes { get; protected set; }

        protected Driver()
        {
        }

        public Driver(User user)
        {
            UserId = user.Id;
            Name = user.UserName;
            Vehicle = Vehicle.Create("Ford", "Focus", 5);
        }

        public void SetVehicle(Vehicle vehicle)
        {
            Vehicle = vehicle;
        }

    }
}
