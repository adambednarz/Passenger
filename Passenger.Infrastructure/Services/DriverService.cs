using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IMapper _mapper;

        public DriverService(IDriverRepository driverRepositiry, IMapper mapper)
        {
            _driverRepository = driverRepositiry;
            _mapper = mapper;
        }

        public async Task<DriverDto> GetAsync(Guid userId)
        {
            var driver = await _driverRepository.GetAsync(userId);
            return  _mapper.Map<Driver, DriverDto>(driver);
        }

        public async Task<DriverDto> GetAsync(string name)
        {  
            var driver = await _driverRepository.GetAsync(name);
            return _mapper.Map<Driver, DriverDto>(driver);
        }
    }
}
