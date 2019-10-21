using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private static readonly ISet<User> _users = new HashSet<User>
        {
            new User("user1@email.com", "user1", "secret", "salt"),
            new User("user2@email.com", "user2", "secret", "salt"),
            new User("user3@email.com", "user23", "secret", "salt"),
            new User("user4@emailcom", "user4", "secret", "salt"),
            new User("user5email.com", "user5", "secret", "salt")
        };

        public async Task<User> GetAsync(Guid id)
        => await Task.FromResult( _users.Single(x => x.Id == id));

        public async Task<User> GetAsync(string email)
        => await Task.FromResult(_users.SingleOrDefault(x => x.Email == email.ToLowerInvariant()));

        public async Task<IEnumerable<User>> GetAllAsync()
        => await Task.FromResult(_users);

        public async Task AddAsync(User user)
        {
            _users.Add(user);
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(Guid id)
        {
            var user = await GetAsync(id);
             _users.Remove(user);
            await Task.CompletedTask;
        }
        
        public async Task UpdateAsync(User user)
        {
            // na razie nic nie robi bo nie posiadamy bazy danych 
            await Task.CompletedTask;
        }
    }
}
