using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Core.Domain
{
    public class Route
    {
        public Guid DriverId { get; protected set; }
        public string Name { get; protected set; }
        public Node StartNode { get; protected set; }
        public Node EndNode { get; protected set; }
        public double Distance { get; protected set; }

        protected Route()
        {
        }

        protected Route(Driver driver, string name, Node start, Node end, double distance)
        {
            DriverId = driver.UserId;
            Name = name;
            StartNode = start;
            EndNode = end;
            Distance = distance;
        }

        public static Route Create(Driver driver, string name, Node start, Node end, double distance)
            => new Route(driver, name, start, end, distance);
    }
}
