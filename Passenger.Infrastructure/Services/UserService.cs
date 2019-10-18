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
        public UserService(IUserRepository userRepositiry)
        {
            _userRepository = userRepositiry;
        }

        public UserDto Get(string email)
        {
            var user = _userRepository.GetAsync(email);
            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FullName = user.FullName
            };
        }

        public void Register(string email, string userName, string password)
        {
            var user = _userRepository.GetAsync(email);
            if (user != null)
            {
                throw new Exception($"User with email: '{email}' already exist.");
            }

            var salt = Guid.NewGuid().ToString("N");
            user = new User(email, userName, password, salt);
            _userRepository.AddAsync(user);
          //  user = new User() 
        }
    }
}
