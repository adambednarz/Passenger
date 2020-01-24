using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.Services.Interfaces;

namespace Passenger.Api.Controllers
{
    [Route("[controller]")]
    public class DriversController : ApiControllerBase
    {
        private readonly IDriverService _driverService;

        public DriversController(ICommandDispatcher commandDispatcher, IDriverService driverService)
            : base(commandDispatcher)

        {
            _driverService = driverService;
        }
        

        [HttpGet]
        public async Task<ActionResult> Browse()
        {
            var drivers = await _driverService.BrowseAsync();
            if (drivers == null)
                return NotFound();

            return Json(drivers);
        }

        [HttpGet("{driverId}")]
        public async Task<IActionResult> Get(Guid driverId)
        {
            var driver = await _driverService.GetAsync(driverId);
            if (driver == null)
                return NotFound();

            return Json(driver);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDriver command)
        {
            await DispatchAsync(command);
            return Created($"/drivers/{command.UserId}", null);
        }

        [Authorize]
        [HttpPut("me")]
        public async Task<IActionResult> Update([FromBody] UpdateDriver command)
        {
            await DispatchAsync(command);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("me")]
        public async Task<IActionResult> Delete()
        {
            await DispatchAsync(new DeleteDriver());
            return NoContent();
        }
    }
}