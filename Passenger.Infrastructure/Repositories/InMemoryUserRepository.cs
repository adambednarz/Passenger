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
            new User(Guid.NewGuid(), "user1@email.com", "user1", "secret", "user"),
            new User(Guid.NewGuid(), "user2@email.com", "user2", "secret", "user"),
            new User(Guid.NewGuid(), "user3@email.com", "user3", "secret", "user")
        };
        

        public async Task<User> GetAsync(Guid id)
        => await Task.FromResult(_users.SingleOrDefault(x => x.Id == id));

        public async Task<User> GetAsync(string email)
        => await Task.FromResult(_users.SingleOrDefault(x => x.Email == email.ToLowerInvariant()));

        public async Task<IEnumerable<User>> GetAllAsync()
        => await Task.FromResult(_users);

        public async Task AddAsync(User user)
        {
            _users.Add(user);
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(User user)
        {
            _users.Remove(user);
            await Task.CompletedTask;
        }
        

        public async Task UpdateAsync(User user)
        {
            var oldUser = await Task.FromResult(_users.First(x => x.Id == user.Id));
            _users.Remove(oldUser);
            _users.Add(user);
        }
    }
}
