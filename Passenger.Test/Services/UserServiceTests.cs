using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using FluentAssertions.AspNetCore.Mvc;
using Passenger.Infrastructure.Services;
using Moq;
using Passenger.Core.Repositories;
using AutoMapper;
using Passenger.Core.Domain;

namespace Passenger.Test.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task Test()
        {
            //Arrange
            var userRapositoryMock = new Mock<IUserRepository>();
            var userService =  new UserService(userRapositoryMock.Object);
            
            // Act 
            await userService.RegisterAsync("test@email.com", "Test", "passwordtest");

            //Assert
            userRapositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }
    }
}
