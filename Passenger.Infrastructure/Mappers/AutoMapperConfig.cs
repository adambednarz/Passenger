using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Driver, DriverDto>();
                cfg.CreateMap<Driver, DriverDetailsDto>();
                cfg.CreateMap<Route, RouteDto>();
                cfg.CreateMap<Node, NodeDto>();
                cfg.CreateMap<Vehicle, VehicleDto>();
                cfg.CreateMap<User, UserDto>();
            })
            .CreateMapper();
    }
}
