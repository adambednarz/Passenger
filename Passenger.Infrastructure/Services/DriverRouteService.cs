using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public class DriverRouteService : IDriverRouteService
    {
        private readonly IDriverRepository _driverRepository;

        public DriverRouteService(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task AddAsync(Guid userId, string name, double startLatitude, 
            double startLongitude, double endLatitude, double endLongitude)
        {
            var driver = await _driverRepository.GetAsync(userId);
            if (driver == null)
                throw new Exception($"Driver with id: '{userId}' was not be found");

            var startNode = Node.Create("Start address", startLatitude, startLongitude);
            var endNode = Node.Create("End address", endLatitude, endLongitude);
            var route = Route.Create(name, startNode, endNode);

            driver.AddRoute(route);
            await _driverRepository.UpdateAsync(driver);
        }

        public async Task DeleteAsync(Guid userId, string name)
        {
            var driver = await _driverRepository.GetAsync(userId);
            if (driver == null)
                throw new Exception($"Driver with id: '{userId}' was not be found");

            driver.DeleteRoute(name);
            await _driverRepository.UpdateAsync(driver);

        }
    }
}
