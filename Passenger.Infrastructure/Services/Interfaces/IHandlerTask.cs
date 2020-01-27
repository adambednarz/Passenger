using Passenger.Core.Exceptions;
using System;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services.Interfaces
{
    public interface IHandlerTask
    {
        IHandlerTask Always(Func<Task> always);
        IHandlerTask OnCustomError(Func<CustomException, Task> onCustomError,
            bool propagateExcetion = false, bool executeOnError = false);
        IHandlerTask OnError(Func<Exception, Task> onError,
            bool propagateExcetion = false, bool executeOnError = false);
        IHandlerTask OnSuccess(Func<Task> onSuccess);
        IHandlerTask PropagateExcption();
        IHandlerTask DoNotPropagateExcption();
        IHandler Next();
        Task ExecuteAsync();
        
    }
}