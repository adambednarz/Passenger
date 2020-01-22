using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services.Interfaces
{
    public interface IDataInitializer : IService
    {
        Task SeedAsync();
    }
}
