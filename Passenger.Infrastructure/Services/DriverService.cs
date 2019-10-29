using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Infrastructure.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IMapper _mapper;

        public DriverService(IDriverRepository driverRepository, IMapper mapper)
        {
            _driverRepository = driverRepository;
            _mapper = mapper;
        }
        public DriverDto Get(Guid userId)
        {
            var driver = _driverRepository.Get(userId);
            return _mapper.Map<Driver, DriverDto>(driver);
        }


    }
}
