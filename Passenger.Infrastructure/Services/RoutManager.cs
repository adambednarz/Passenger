using Passenger.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public class RoutManager : IRouteManager
    {
        private static readonly Random _random = new Random();
        public async Task<string> GetAddresAsync(double latitude, double longitude)
            => await Task.FromResult($"Sample adress: {_random.Next(0, 100)}");

        public double CalculateDistance(double startLatitude, double startLongitude, double endLatitude, double endLongitude)
            => _random.Next(500, 10000);
    }
}
