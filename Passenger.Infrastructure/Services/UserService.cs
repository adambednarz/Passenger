using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepositiry, IMapper mapper)
        {
            _userRepository = userRepositiry;
            _mapper = mapper;
        }

        public UserDto Get(string email)
        {
            var user = _userRepository.Get(email);

            return _mapper.Map<User, UserDto>(user);
            
            
            //return new UserDto
            //{
            //    Id = user.Id,
            //    UserName = user.UserName,
            //    Email = user.Email,
            //    FullName = user.FullName
            //};
        }

        public void Register(string email, string userName, string password)
        {
            var user = _userRepository.Get(email);
            if (user != null)
            {
                throw new Exception($"User with email: '{email}' already exist.");
            }

            var salt = Guid.NewGuid().ToString("N");
            user = new User(email, userName, password, salt);
            _userRepository.Add(user);
          //  user = new User() 
        }
    }
}
