using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Passenger.Infrastructure.Repositories
{
    class InMemoryDriverRepository : IDriverRepository
    {
        private static ISet<Driver> _drivers = new HashSet<Driver>();

        public void Add(Driver driver)
        {
            _drivers.Add(driver);
        }

        public Driver Get(Guid userId)
            => _drivers.Single(x => x.UserId == userId);
        

        public IEnumerable<Driver> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Driver driver)
        {
            throw new NotImplementedException();
        }
    }
}
