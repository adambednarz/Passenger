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
        public async Task registeer_asyn_shoulf_invoke_addsync_on_repository()
        {
            //Arrange
            var userRapositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var userService =  new UserService(userRapositoryMock.Object, mapperMock.Object);
            
            // Act 
            await userService.RegisterAsync("test@email.com", "Test", "passwordtest");

            //Assert
            userRapositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }
    }
}
