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

        public Task AddAsync(User user)
        {
            _users.Add(user);
        }

        public Task<User> GetAsync(Guid id)
        => _users.Single(x => x.Id == id);

        public Task<User> GetAsync(string email)
        => _users.SingleOrDefault(x => x.Email == email.ToLowerInvariant());

        public Task<IEnumerable<User>> GetAllAsync()
        => _users;

        public Task RemoveAsync(Guid id)
        {
            var user = GetAsync(id);
            _users.Remove(user);
        }
        
        public Task UpdateAsync(User user)
        {
            // na razie nic nie robi bo nie posiadamy bazy danych 
        }
    }
}
