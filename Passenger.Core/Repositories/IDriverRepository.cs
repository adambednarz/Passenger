using Passenger.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Core.Repositories
{
    public interface IDriverRepository : IRepository
    {
        Driver Get(Guid userId);
        IEnumerable<Driver> GetAll();
        void Add(Driver driver);
        void Update(Driver driver);
    }
}
