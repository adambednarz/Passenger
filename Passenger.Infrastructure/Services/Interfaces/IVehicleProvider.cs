using Passenger.Core.Domain;
using Passenger.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services.Interfaces
{
    public interface IVehicleProvider : IService
    {
        Task<IEnumerable<VehicleDto>> BrowseAsync();
        Task<VehicleDto> GetAsync(string brand, string model);
    }
}
