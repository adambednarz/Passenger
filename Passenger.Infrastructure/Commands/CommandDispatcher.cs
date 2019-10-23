using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _commponentContext;
        public CommandDispatcher(IComponentContext componentContext)
        {
            _commponentContext = componentContext;
        }
        public async Task DispatcherAsync<T>(T command) where T : ICommand
        {
            if(_commponentContext == null)
            {
                throw new ArgumentNullException(nameof(command),
                    $"Command {typeof(T).Name} can not be null");
            }
            var handler = _commponentContext.Resolve<ICommandHandler<T>>();
            await handler.HandleAsync(command);
        }
    }
}
