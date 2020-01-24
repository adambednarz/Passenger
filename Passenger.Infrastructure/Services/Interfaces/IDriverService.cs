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
        Task<DriverDetailsDto> GetAsync(Guid userId);
        Task CreateAsync(Guid userId);
        Task SetVehicleAsync(Guid userId, string brand, string model);
        Task DeleteAsync(Guid userId);
    }
}
