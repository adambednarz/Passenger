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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IEncrypter _encrypter;

        public UserService(IUserRepository userRepositiry,
            IEncrypter encrypter, IMapper mapper)
        {
            _userRepository = userRepositiry;
            _mapper = mapper;
            _encrypter = encrypter;
        }

        public async Task<IEnumerable<UserDto>> BrowseAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);

        }

        public async Task<UserDto> GetAsync(string email)
        {
            var user = await _userRepository.GetAsync(email);
            return _mapper.Map<User, UserDto>(user);
        }

        public async Task LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
            {
                throw new Exception("Invalid credentioal");
            }

            var hash = _encrypter.GetHash(password, user.Salt);
            if (user.Password == hash)
            {
                //var CorrectPassword = true;
                return;
            }
            throw new Exception("Invalid credentioal");

        }

        public async Task  RegisterAsync(Guid userId, string email, string userName, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
            {
                throw new Exception($"User with email: '{email}' already exist.");
            }

            //var salt = Guid.NewGuid().ToString("N");          <-- implementacja bez Iencryptera
            var salt = _encrypter.GetSalt(password);
            var hash = _encrypter.GetHash(password, salt);
            //user = new User(email, userName, password);      <-- implementacja bez Iencryptera
            user = new User(userId, email, userName, hash);
            await _userRepository.AddAsync(user);
          //  user = new User() 
        }
    }
}
