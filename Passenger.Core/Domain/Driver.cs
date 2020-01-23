using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Passenger.Core.Domain
{
    public class Driver
    {
        private static readonly ISet<Route> _routes = new HashSet<Route>();
        
        public Guid UserId { get; protected set; }
        public string Name { get; protected set; }
        public Vehicle Vehicle { get; protected set; }
        public IEnumerable<Route> Routes => _routes;
        public IEnumerable<DailyRoute> DailyRoutes { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        protected Driver()
        {
        }

        public Driver(User user)
        {
            UserId = user.Id;
            Name = user.UserName;
            UpdatedAt = DateTime.Now;
        }

        public void SetVehicle(Vehicle vehicle)
        {
            Vehicle = vehicle;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddRoute(Route route)
        {
            var routeIs = _routes.SingleOrDefault(x => x.Name == route.Name);
            if (routeIs != null)
                throw new Exception($"Route with name '{route.Name}' already exist for driver:  '{Name}'");
            _routes.Add(route);
            UpdatedAt = DateTime.UtcNow;
        }

        public void DeleteRoute(string name)
        {
            var route = _routes.SingleOrDefault(x => x.Name == name);
            if (route == null)
                throw new Exception($"Route with name: '{name}' could not be delete because it not exist");
            _routes.Remove(route);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
