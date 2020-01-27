using Passenger.Core.Exceptions;
using Passenger.Infrastructure.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public class HandlerTask : IHandlerTask
    {
        private readonly IHandler _handler;
        private readonly Func<Task> _run;
        private Func<Task>  _validate;
        private Func<Task>  _always;
        private Func<Task>  _onSuccess;
        private Func<Exception, Task>  _onError;
        private Func<CustomException, Task>  _onCustomeError;
        private bool _propagateException = true;
        private bool _executeOnError = true;

        public HandlerTask(IHandler handler, Func<Task> run, Func<Task> validate = null)
        {
            _handler = handler;
            _run = run;
            _validate = validate;
        }


        public IHandlerTask Always(Func<Task> always)
        {
            _always = always;
            return this;
        }

        public IHandler Next() => _handler;

        public IHandlerTask OnCustomError(Func<CustomException, Task> onCustomError, bool propagateExcetion = false, bool executeOnError = false)
        {
            _onCustomeError = onCustomError;
            _propagateException = propagateExcetion;
            _executeOnError = executeOnError;
            return this;
        }
        public IHandlerTask OnError(Func<Exception, Task> onError, bool propagateExcetion = false, bool executeOnError = false)
        {
            _onError = onError;
            _propagateException = propagateExcetion;
            _executeOnError = executeOnError;
            return this;
        }

        public IHandlerTask OnSuccess(Func<Task> onSuccess)
        {
            _onSuccess = onSuccess;
            return this;
        }

        public IHandlerTask PropagateExcption()
        {
            _propagateException = true;
            return this;
        }
        public IHandlerTask DoNotPropagateExcption()
        {
            _propagateException = false;
            return this;
        }

        public async Task ExecuteAsync()
        {
            try
            {
                if (_validate != null)
                    await _validate();
                await _run();
                if (_onSuccess != null)
                    await _onSuccess();
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(exception);
                if (_propagateException)
                    throw;
            }
            finally
            {
                if (_always != null)
                    await _always();
            }
        }

        private async Task HandleExceptionAsync(Exception exception)
        {
            var customException = exception as CustomException;
            if(customException != null)
            {
                if (_onCustomeError != null)
                    await _onCustomeError(customException);
            }

            var executeOnError = _executeOnError || customException == null;
            if(!executeOnError)
            {
                return;
            }
            if(_onError != null)
            {
                await _onError(exception);
            }
        }
    }
}