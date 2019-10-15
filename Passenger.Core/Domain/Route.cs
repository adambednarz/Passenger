using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Core.Domain
{
    public class Route
    {
        public Guid Id { get; protected set; }
        public Node StartNode { get; protected set; }
        public Node EndNode{ get; protected set; }

        public Route()
        {
        }
    }
}
