using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
