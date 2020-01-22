using Microsoft.Extensions.Caching.Memory;
using Passenger.Core.Domain;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public class VehicleProvider : IVehicleProvider
    {
        private static readonly IDictionary<string, IEnumerable<VehicleDetails>> _availableVehicles =
            new Dictionary<string, IEnumerable<VehicleDetails>>
            {
                ["BMW"] = new List<VehicleDetails>
                {
                    new VehicleDetails("i8", 3),
                    new VehicleDetails("E36", 5),
                    new VehicleDetails("X5", 5)
                },
                ["Ford"] = new List<VehicleDetails>
                {
                    new VehicleDetails("Fiesta", 5),
                },
                ["Skoda"] = new List<VehicleDetails>
                {
                    new VehicleDetails("Fabia", 5),
                    new VehicleDetails("Octavia", 5)
                },
                ["Volkswagen"] = new List<VehicleDetails>()
            };

        private readonly IMemoryCache _cache;
        private static readonly string _cacheKey = "vehicles";

        public VehicleProvider(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }


        public async Task<IEnumerable<VehicleDto>> BrowseAsync()
        {
            var vehicles =  _cache.Get<IEnumerable<VehicleDto>>(_cacheKey);
            if(vehicles == null)
            {
                vehicles = await GetAllAsync();
                _cache.Set(_cacheKey, vehicles);
            }
            
            return vehicles;
        }

        public async Task<VehicleDto> GetAsync(string brand, string model)
        {
            if (!_availableVehicles.ContainsKey(brand))
                throw new Exception($"Vehicle brand: '{brand}' is not available.");

            var vehicles = _availableVehicles[brand];
            var vehicle = await Task.FromResult(vehicles.SingleOrDefault(x => x.Model == model));
            if (vehicle == null)
                throw new Exception($"Vehicle '{model}' is not avaiable for '{brand}' brand.");

            return new VehicleDto
            {
                Brand = brand,
                Model = vehicle.Model,
                Seats = vehicle.Seats
            };
        }

        public async Task<IEnumerable<VehicleDto>> GetAllAsync()
            => await Task.FromResult(_availableVehicles.GroupBy(x => x.Key)
                .SelectMany(y => y.SelectMany(g => g.Value.Select(v => new VehicleDto
            {
                Brand = g.Key,
                Model = v.Model,
                Seats = v.Seats
            }))));

        private class VehicleDetails
        {
            public string Model { get; }
            public int Seats { get; }

            public VehicleDetails(string model, int seats)
            {
                Model = model;
                Seats = seats;
            }
        }
    }
}
