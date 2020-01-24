using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Extensions;
using Passenger.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public class DriverRouteService : IDriverRouteService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IMapper _mapper;
        private readonly IRouteManager _routeManager;

        public DriverRouteService(IDriverRepository driverRepository,
            IMapper mapper,
            IRouteManager routeManager)
        {
            _driverRepository = driverRepository;
            _mapper = mapper;
            _routeManager = routeManager;
        }

        public async Task AddAsync(Guid userId, string name, double startLatitude, 
            double startLongitude, double endLatitude, double endLongitude)
        {
            var driver = await _driverRepository.GetDriverOrFailAsync(userId);
            var startNodeName = await _routeManager.GetAddresAsync(startLatitude, startLongitude);
            var endNodeName = await _routeManager.GetAddresAsync(endLatitude, endLongitude);
            var startNode = Node.Create(startNodeName, startLatitude, startLongitude);
            var endNode = Node.Create(endNodeName, endLatitude, endLongitude);
            var distance = _routeManager.CalculateDistance(startLongitude, startLongitude, endLatitude, endLongitude);
            driver.AddRoute(name, startNode, endNode, distance);
            await _driverRepository.UpdateAsync(driver);
        }

        public async Task<IEnumerable<RouteDto>> BrowseRoutesForDriver(Guid userId)
        {
            var driver = await _driverRepository.GetDriverOrFailAsync(userId);
            var routes = driver.Routes.Where(x => x.DriverId == driver.UserId);
            return _mapper.Map<IEnumerable<RouteDto>>(routes);
        }

        public async Task DeleteAsync(Guid userId, string name)
        {
            var driver = await _driverRepository.GetDriverOrFailAsync(userId);
            driver.DeleteRoute(name);
            await _driverRepository.UpdateAsync(driver);

        }
    }
}
