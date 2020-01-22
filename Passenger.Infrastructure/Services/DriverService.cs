using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IVehicleProvider _vehicleProvider;

        public DriverService(IDriverRepository driverRepository, IMapper mapper, 
            IUserRepository userRepository, IVehicleProvider vehicleProvider)
        {
            _driverRepository = driverRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _vehicleProvider = vehicleProvider;
        }
        
        public async Task<IEnumerable<DriverDto>> BrowseAsync()
        {
            var drivers = await _driverRepository.BrowseAsync();
            return _mapper.Map<IEnumerable<DriverDto>>(drivers);
        } 

        public async Task<DriverDto> GetAsync(Guid userId)
        {
            var driver = await _driverRepository.GetAsync(userId);
            return _mapper.Map<Driver, DriverDto>(driver);
        }

        public async Task CreateAsync(Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null)
                throw new Exception($"User with id: {userId} does not found");

            var driver = await _driverRepository.GetAsync(userId);
            if (driver != null)
                throw new Exception($"Driver with id: {userId} already exist");

            driver = new Driver(user);
            await _driverRepository.AddAsync(driver);
        }

        public async Task AddVehicleAsync(Guid userId, string brand, string model)
        {
            var driver = await _driverRepository.GetAsync(userId);
            if (driver == null)
                throw new Exception($"Driver with id: '{userId}' does not exist");

            var vehicleDetails = await _vehicleProvider.GetAsync(brand, model);
            var vehicle =Vehicle.Create(vehicleDetails.Brand, vehicleDetails.Model, vehicleDetails.Seats);
            driver.SetVehicle(vehicle);
            await _driverRepository.UpdateAsync(driver);
        }
    }
}
