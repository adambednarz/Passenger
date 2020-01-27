using Microsoft.Extensions.Caching.Memory;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Accounts;
using Passenger.Infrastructure.Extensions;
using Passenger.Infrastructure.Services.Interfaces;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Handlers.Accounts
{
    public class LoginHandlers : ICommandHandler<Login>
    {
        private readonly IMemoryCache _cache;
        private readonly IHandler _handler;
        private readonly IJwtHandler _jwtHandler;
        private readonly IUserService _userService;

        public LoginHandlers(IMemoryCache cache, IHandler handler,
            IJwtHandler jwtHandler, IUserService userService)
        {
            _cache = cache;
            _handler = handler;
            _jwtHandler = jwtHandler;
            _userService = userService;
        }

        public async Task HandleAsync(Login command)
            => await _handler
                    .Run(async () => await _userService.LoginAsync(command.Email, command.Password))
                    .Next()
                    .Run(async () =>
                    {
                        var user = await _userService.GetAsync(command.Email);
                        var jwt = _jwtHandler.CreateToken(user.Id, user.Role);
                        _cache.SetJwt(command.TokenId, jwt);
                    })
                    .ExecuteAsync();  
    }
}
