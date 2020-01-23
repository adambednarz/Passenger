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
using Passenger.Infrastructure.Services.Interfaces;

namespace Passenger.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task register_async_should_invoke_add_asyn_on_repository()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            encrypterMock.Setup(x => x.GetSalt(It.IsAny<string>())).Returns("hash");
            encrypterMock.Setup(x => x.GetHash(It.IsAny<string>(), It.IsAny<string>())).Returns("salt");
            var mapperMock = new Mock<IMapper>();

            var userService = new UserService(userRepositoryMock.Object, encrypterMock.Object, mapperMock.Object);
            await userService.RegisterAsync(Guid.NewGuid(), "user@email.com", "user1", "user", "secret");

            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task when_calling_get_async_for_existing_user_it_should_invoke_get_async_on_user_repository()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var encrypterMock = new Mock<IEncrypter>();

            var userService = new UserService(userRepositoryMock.Object, encrypterMock.Object, mapperMock.Object);
            await userService.GetAsync("user1@email.com");

            var user = new User(Guid.NewGuid(), "user1@email.com", "user1", "user", ".............", "secret");

            userRepositoryMock.Verify(x => x.GetAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task when_calling_get_async_and_user_does_not_exist_it_should_invoke_user_repository_get_async()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            var mapperMock = new Mock<IMapper>();

            var userService = new UserService(userRepositoryMock.Object, encrypterMock.Object, mapperMock.Object);
            await userService.GetAsync("user@email.com");

            userRepositoryMock.Setup(x => x.GetAsync("user@email.com"))
                              .ReturnsAsync(() => null);

            userRepositoryMock.Verify(x => x.GetAsync(It.IsAny<string>()), Times.Once());
        }
    }
}
