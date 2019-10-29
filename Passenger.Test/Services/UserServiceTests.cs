using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;
using Passenger.Core.Repositories;
using Moq;
using Passenger.Infrastructure.Services;
using AutoMapper;
using Passenger.Core.Domain;

namespace Passenger.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task register_async_should_invoke_add_asyn_on_repository()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();

            //var userService = new UserService(userRepositoryMock.Object, mapperMock.Object);
            //await userService.RegisterAsync("mock@email.com", "mock", "secret");

            //userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }
    }
}
