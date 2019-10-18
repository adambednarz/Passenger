using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Infrastructure.Mapper
{
    public static class MapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<Driver, DriverDto>();
                cfg.CreateMap<Vehicle, VehicleDto>() // weź x1 i zmapuj do x2
                .ForMember(x => x.Model, m => m.MapFrom(p => p.Name)); 
            })
            .CreateMapper();
    }
}
