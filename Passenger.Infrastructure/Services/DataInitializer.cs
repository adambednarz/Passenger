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
        private readonly IDriverRouteService _driverRouteService;
        private readonly ILogger<DataInitializer> _logger;

        public DataInitializer(IUserService userService, ILogger<DataInitializer> logger,
            IDriverService driverService, IDriverRouteService driverRouteService)
        {
            _userService = userService;
            _logger = logger;
            _driverService = driverService;
            _driverRouteService = driverRouteService;
        }

        public async Task SeedAsync()
        {
            _logger.LogInformation("Initializing data......");
            var tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                var userId = Guid.NewGuid();
                tasks.Add(_userService.RegisterAsync(userId, $"user{i + 1}@email.com", $"User{i + 1}", "user", "password"));
                tasks.Add(_driverService.CreateAsync(userId));
                tasks.Add(_driverService.SetVehicleAsync(userId, "BMW", "X5"));
                tasks.Add(_driverRouteService.AddAsync(userId, "Job route", 10, 10, 50, 50));
                tasks.Add(_driverRouteService.AddAsync(userId, "Gym route", 13, 11, 55, 77));
            }
            for (int i = 0; i < 3; i++)
            {
                var userId = Guid.NewGuid();
                tasks.Add(_userService.RegisterAsync(userId, $"admin{i + 1}@admin.com", $"Admin{i + 1}", "user", "password"));
            }

            await Task.WhenAll(tasks);
            _logger.LogInformation("Data was initialized.");
        }
    }
}
