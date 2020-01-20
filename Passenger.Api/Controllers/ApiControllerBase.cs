using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;

namespace Passenger.Api.Controllers
{
    public abstract class ApiControllerBase : Controller
    {
        protected readonly ICommandDispatcher CommandDispatcher;
        public ApiControllerBase(ICommandDispatcher commandDispatcher)
        {
            CommandDispatcher = commandDispatcher;

        }
    }
}
