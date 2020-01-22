﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Passenger.Core.Domain
{
    public class DailyRoute
    {
        private readonly ISet<PassengerNode> _passengerNodes = new HashSet<PassengerNode>();
        public Guid Id { get; protected set; }
        public Route Route { get; protected set; }
        public IEnumerable<PassengerNode> PassengerNodes => _passengerNodes;

        public DailyRoute()
        {
            Id = Guid.NewGuid();
        }

        public void AddPassengerNode(Passenger passenger, Node node)
        {
            var passengerNode = GetPassengerNode(passenger);
            if(passengerNode != null)
            {
                throw new InvalidOperationException($"Node actually exist for passenger {passenger.UserId}");
            }

            _passengerNodes.Add(PassengerNode.Create(passenger, node));
        }

        public void RemovePassengerNode(Passenger passenger)
        {
            var passengerNodeDB = GetPassengerNode(passenger);
            if (passengerNodeDB == null)
            {
                return;
            }
            _passengerNodes.Remove(passengerNodeDB);
        }

        public PassengerNode GetPassengerNode(Passenger passenger)
        => _passengerNodes.SingleOrDefault(x => x.Passenger.UserId == passenger.UserId);
    }
}
