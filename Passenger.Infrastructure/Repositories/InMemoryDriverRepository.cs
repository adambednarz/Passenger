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
        private static ISet<Driver> _drivers = new HashSet<Driver>
            {
                new Driver("Focus"),
            };

        public Task AddAsync(Driver driver)
        {
            _drivers.Add(driver);
        }

        public Task<Driver> GetAsync(Guid userId)
            => _drivers.Single(x => x.UserId == userId);

        public Task<Driver> GetAsync(string name)
            => _drivers.SingleOrDefault(x => x.Name == name);

        public Task<IEnumerable<Driver>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Driver driver)
        {
            throw new NotImplementedException();
        }
    }
}
