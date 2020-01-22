using Microsoft.Extensions.Caching.Memory;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.Extensions;
using Passenger.Infrastructure.Services;
using Passenger.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Handlers.Users
{
    public class LoginHandlers : ICommandHandler<Login>
    {
        private readonly IUserService _userService;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMemoryCache _cache;

        public LoginHandlers(IUserService userService, IJwtHandler jwtHandler,
            IMemoryCache cache)
        {
            _userService = userService;
            _jwtHandler = jwtHandler;
            _cache = cache;
        }

        public async Task HandleAsync(Login command)
        {
            await _userService.LoginAsync(command.Email, command.Password);
            var user = await _userService.GetAsync(command.Email);
            var jwt = _jwtHandler.CreateToken(user.Email, user.Role);
            _cache.SetJwt(command.TokenId, jwt);
        }
    }
}
