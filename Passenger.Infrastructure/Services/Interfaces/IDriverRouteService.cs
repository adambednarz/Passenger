using System;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services.Interfaces
{
    public interface IDriverRouteService : IService
    {
        Task AddAsync(Guid userId, string name,
            double startLatitude, double startLongitude,
            double endLatitude, double endLongitude);

        Task DeleteAsync(Guid userId, string name);
    }
}
