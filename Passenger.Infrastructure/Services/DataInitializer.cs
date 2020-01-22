using Microsoft.Extensions.Logging;
using Passenger.Core.Domain;
using Passenger.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService _userService;
        private readonly IDriverService _driverService;
        private readonly ILogger<DataInitializer> _logger;

        public DataInitializer(IUserService userService, ILogger<DataInitializer> logger,
            IDriverService driverService)
        {
            _userService = userService;
            _logger = logger;
            _driverService = driverService;
        }

        public async Task SeedAsync()
        {
            _logger.LogInformation("Initializing data......");
            var tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                var userId = Guid.NewGuid();
                tasks.Add(_userService.RegisterAsync(userId, $"user{i + 1}@email.com", $"User{i + 1}", "password"));
                tasks.Add(_driverService.CreateAsync(userId));
                tasks.Add(_driverService.AddVehicleAsync(userId, "BMW", "X5"));
            }
            for (int i = 0; i < 3; i++)
            {
                var userId = Guid.NewGuid();
                tasks.Add(_userService.RegisterAsync(userId, $"admin{i + 1}@admin.com", $"Admin{i + 1}", "password"));
            }

            await Task.WhenAll(tasks);
            _logger.LogInformation("Data was initialized.");
        }
    }
}
