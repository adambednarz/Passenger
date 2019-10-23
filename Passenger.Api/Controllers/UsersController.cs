using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Services;

namespace Passenger.Api.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICommandDispatcher _commandDispatcher;

        public UsersController(IUserService userService, ICommandDispatcher commandDispatcher )
        {
            _userService = userService;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            var user = await _userService.GetAsync(email);
            if(user == null)
            {
                return NotFound();
            }
            return Json(user);
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] CreateUser command)
        {
            //await _userService.RegisterAsync(command.Email, command.UserName, command.Password);

            // użycie Autofac
            await _commandDispatcher.DispatcherAsync(command);  

            return Created($"users/{command.Email}", new object());
        }
    }
}