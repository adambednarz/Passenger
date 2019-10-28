using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Passenger.Core.Domain
{
    public class DailyRoute
    {
        private ISet<PassengerNode> _passengerNodes = new HashSet<PassengerNode>();
        public Guid Id { get; protected set; }
        public Route Route { get; protected set; }
        public IEnumerable<PassengerNode> PassengerNodes => _passengerNodes;

        public DailyRoute()
        {
            Id = Guid.NewGuid();
        }

        public void AddPassengerNode(Passenger passenger, Node node)
        {
            var passengerNodeDB = Get(passenger);
            if(passengerNodeDB != null)
            {
                throw new InvalidOperationException($"Node actually exist for passenger {passenger.UserId}");
            }
                var passengerNode = PassengerNode.Create(passenger, node);
                _passengerNodes.Add(passengerNode);
        }

        public void RemovePassengerNode(Passenger passenger)
        {
            var passengerNodeDB = Get(passenger);
            if (passengerNodeDB == null)
            {
                return;
            }
            _passengerNodes.Remove(passengerNodeDB);
        }

        public PassengerNode Get(Passenger passenger)
        => _passengerNodes.SingleOrDefault(x => x.Passenger.UserId == passenger.UserId);
    }
}
