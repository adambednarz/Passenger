using Passenger.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Passenger.Infrastructure.Repositories;
using Passenger.Core.Domain;
using FluentAssertions;
using Xunit;

namespace Passenger.Test.Repositories
{
    public class InMemoryDriverRepositoryTests
    {
        [Fact]
        public async Task AddAsync_ValidDriver_CorrectlyAddedDriverToList()
        {
            //Arrange
            var id = Guid.NewGuid();
            var user = new User(id, "email@email.com", "User1", "secret", "user");
            var driver = new Driver(user);
            IDriverRepository driverRepository = new InMemoryDriverRepository();

            //Act
            await driverRepository.AddAsync(driver);

            //Assert
            var existingDriver = await driverRepository.GetAsync(id);
            existingDriver.Should().BeEquivalentTo(driver);
        }

        [Fact]
        public async Task UpdateAsync_ModifiedDriver_CorrectlyUpdatedDriver()
        {
            //Arrange
            var id = Guid.NewGuid();
            var user = new User(id, "email@email.com", "User1", "secret", "user");
            var updatedUser = new User(id, "updated@email.com", "Updated1", "secret", "user");
            var driver = new Driver(user);
            var updatedDriver = new Driver(updatedUser);
            IDriverRepository driverRepository = new InMemoryDriverRepository();

            //Act
            await driverRepository.AddAsync(driver);
            await driverRepository.UpdateAsync(updatedDriver);


            //Assert
            var existingDriver = await driverRepository.GetAsync(id);
            existingDriver.Should().BeEquivalentTo(updatedDriver);
        }
    }
}
