using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Extensions;
using Passenger.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IDriverRouteService _driverRouteService;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IVehicleProvider _vehicleProvider;

        public DriverService(IDriverRepository driverRepository, 
            IDriverRouteService driverRouteService,
            IMapper mapper, 
            IUserRepository userRepository, 
            IVehicleProvider vehicleProvider)
        {
            _driverRepository = driverRepository;
            _driverRouteService = driverRouteService;
            _mapper = mapper;
            _userRepository = userRepository;
            _vehicleProvider = vehicleProvider;
        }
        
        public async Task<IEnumerable<DriverDto>> BrowseAsync()
        {
            var drivers = await _driverRepository.BrowseAsync();
            return _mapper.Map<IEnumerable<DriverDto>>(drivers);
        } 

        public async Task<DriverDetailsDto> GetAsync(Guid userId)
        {
            var driver = await _driverRepository.GetDriverOrFailAsync(userId);
            var routes = await _driverRouteService.BrowseRoutesForDriver(userId);
            var driverDetails = _mapper.Map<Driver, DriverDetailsDto>(driver);
            driverDetails.Routes = routes;
            return driverDetails;
        }

        public async Task CreateAsync(Guid userId)
        {
            var user = await _userRepository.GetUserOrFailAsync(userId);
            var driver = await _driverRepository.GetAsync(userId);
            if (driver != null)
                throw new Exception($"Driver with id: {userId} already exist");

            driver = new Driver(user);
            await _driverRepository.AddAsync(driver);
        }

        public async Task SetVehicleAsync(Guid userId, string brand, string model)
        {
            var driver = await _driverRepository.GetDriverOrFailAsync(userId);
            var vehicleDetails = await _vehicleProvider.GetAsync(brand, model);
            var vehicle = Vehicle.Create(vehicleDetails.Brand, vehicleDetails.Model, vehicleDetails.Seats);
            driver.SetVehicle(vehicle);
            await _driverRepository.UpdateAsync(driver);
        }

        public async Task DeleteAsync(Guid userId)
        {
            var driver = await _driverRepository.GetDriverOrFailAsync(userId);
            await _driverRepository.DeleteAsync(driver);
        }
    }
}
