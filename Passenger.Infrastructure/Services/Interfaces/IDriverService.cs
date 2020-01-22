using Passenger.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services.Interfaces
{
    public interface IDriverService : IService
    {
        Task<IEnumerable<DriverDto>> BrowseAsync();
        Task<DriverDto> GetAsync(Guid userId);
        Task CreateAsync(Guid userId);
        Task AddVehicleAsync(Guid userId, string brand, string name);
    }
}
