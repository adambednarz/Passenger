using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Commands
{
    public interface ICommandDispatcher 
    {
        Task DispatcherAsync<T>(T command) where T : ICommand;
    }
}
