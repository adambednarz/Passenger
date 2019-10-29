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
            {};

        public async Task AddAsync(Driver driver)
        {
            _drivers.Add(driver);
            await Task.CompletedTask;
        }

        public async Task<Driver> GetAsync(Guid userId)
            => await Task.FromResult(_drivers.SingleOrDefault(x => x.UserId == userId));
        

        public IEnumerable<Driver> GetAll()
            => _drivers;


        public async Task UpdateAsync(Driver driver)
        {
           // throw new NotImplementedException();
            await Task.CompletedTask;
        }

        Task<Driver> IDriverRepository.GetAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
