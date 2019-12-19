using FluentAssertions;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Passenger.Test.Repositories
{
    public class InMemoryUserRepositoryTests
    {
        [Fact]
        public async Task AddAsync_ValidUserObject_CorrectlyAddedUserToList()
        {
            //Arrange
            var id = Guid.NewGuid();
            var user = new User(id, "email@email.com", "User1", "secret", "user");
            IUserRepository userRepository = new InMemoryUserRepository();

            //Act 
            await userRepository.AddAsync(user);
            //Assert
            var existingUser = await userRepository.GetAsync(id);
            existingUser.Should().BeEquivalentTo(user);
        }

        [Fact]
        public async Task RemoveAsync_ExistingUser_RemoveUser()
        {
            //Arrange
            var id = Guid.NewGuid();
            var user = new User(id, "email@email.com", "User1", "secret", "user");

            IUserRepository userRepository = new InMemoryUserRepository();

            //Act 
            await userRepository.AddAsync(user);
            await userRepository.RemoveAsync(user);
            //Assert
            var existingUser = await userRepository.GetAsync(id);
            existingUser.Should().BeNull();
        }

        [Fact]
        public async Task UpdateAsync_ModifiedUser_CorrectlyUpdatedUser()
        {
            //Arrange
            var id = Guid.NewGuid();
            var user = new User(id, "email@email.com", "User1", "secret", "user");
            var updatedUser = new User(id, "updated@email.com", "Updated1", "secret", "user");
            IUserRepository userRepository = new InMemoryUserRepository();

            //Act 
            await userRepository.AddAsync(user);
            await userRepository.UpdateAsync(updatedUser);
            //Assert
            var existingUser = await userRepository.GetAsync(id);
            existingUser.Should().BeEquivalentTo(updatedUser);
        }
    }
}
