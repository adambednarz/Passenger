using Passenger.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services.Interfaces
{
    public interface IDriverRouteService : IService
    {
        Task<IEnumerable<RouteDto>> BrowseRoutesForDriver(Guid userId);
        Task AddAsync(Guid userId, string name,
            double startLatitude, double startLongitude,
            double endLatitude, double endLongitude);

        Task DeleteAsync(Guid userId, string name);
    }
}
