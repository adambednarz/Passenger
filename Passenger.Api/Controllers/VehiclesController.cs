using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Services.Interfaces;

namespace Passenger.Api.Controllers
{
    [Route("[controller]")]
    public class VehiclesController : ApiControllerBase
    {
        private readonly IVehicleProvider _vehicleProvider;

        public VehiclesController(ICommandDispatcher commandDispatcher,
            IVehicleProvider vehicleProvider) : base(commandDispatcher)
        {
            _vehicleProvider = vehicleProvider;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var vehicles = await _vehicleProvider.BrowseAsync();
            return Json(vehicles);
        }
    }
}