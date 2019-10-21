using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Repositories
{
    public class InMemoryDriverRepository : IDriverRepository
    {
        private static readonly ISet<Driver> _drivers = new HashSet<Driver>
            {
                new Driver("Focus"),
            };

        public async Task AddAsync(Driver driver)
        {
            _drivers.Add(driver);
            await Task.CompletedTask;
        }

        public async Task<Driver> GetAsync(Guid userId)
            => await Task.FromResult(_drivers.Single(x => x.UserId == userId));

        public async Task<Driver> GetAsync(string name)
            => await Task.FromResult(_drivers.SingleOrDefault(x => x.Name == name));

        public async Task<IEnumerable<Driver>> GetAllAsync()
            => await Task.FromResult(_drivers);

        public async Task UpdateAsync(Driver driver)
        {
           // throw new NotImplementedException();
            await Task.CompletedTask;
        }
    }
}
