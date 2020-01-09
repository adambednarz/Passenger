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
        private static readonly ISet<Driver> _drivers = new HashSet<Driver>();

        public async Task AddAsync(Driver driver)
            => await Task.FromResult(_drivers.Add(driver));
        

        public async Task<Driver> GetAsync(Guid userId)
            => await Task.FromResult(_drivers.SingleOrDefault(x => x.UserId == userId));
        

        public async Task<IEnumerable<Driver>> GetAllAsync()
            => await Task.FromResult(_drivers);


        public async Task UpdateAsync(Driver driver)
        {
            var oldDriver = await Task.FromResult(_drivers.First(x => x.UserId == driver.UserId));
            _drivers.Remove(oldDriver);
            _drivers.Add(driver);
        }
    }
}
