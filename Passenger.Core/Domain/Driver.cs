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

        public Driver(string name)
        {
            UserId = Guid.NewGuid();
            SetName(name);
            Vehicle = Vehicle.SetVehicle("Ford", "Focus", 4);
        }

        void SetName(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new Exception("Name can not be empty.");
            }
            if(Name == name)
            {
                return;
            }
            Name = name;
        }


    }
}
