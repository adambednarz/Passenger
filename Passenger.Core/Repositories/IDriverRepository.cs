
using Passenger.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Core.Repositories
{
    public interface IDriverRepository : IRepository
    {
        Task<IEnumerable<Driver>> BrowseAsync();
        Task<Driver> GetAsync(Guid userId);
        Task AddAsync(Driver driver);
        Task UpdateAsync(Driver driver);
    }
}
