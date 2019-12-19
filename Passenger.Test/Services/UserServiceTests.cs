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
using FluentAssertions;
using Passenger.Infrastructure.DTO;

namespace Passenger.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task RegisterAsync_ValidParameters_InvokeAddAsyncOnRepository()
        {
            //Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var jwtHandlerMock = new Mock<IJwtHandler>();

            Guid id = Guid.NewGuid();
            var email = "email@email.com";
            var password = "secret";

            var encrypterMock = new Mock<IEncrypter>();
            encrypterMock.Setup(x => x.GetSalt(password)).Returns("salt");
            encrypterMock.Setup(x => x.GetHash(password, "salt")).Returns("hash");
 
            var userService = new UserService(userRepositoryMock.Object, encrypterMock.Object,
                    mapperMock.Object, jwtHandlerMock.Object);
            //Act
            await userService.RegisterAsync(id, email, "UserName", password, "user");
            //Assert
            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_ValidUserCredentials_InvokeCreateToken_()
        {
            //Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var encrypterMock = new Mock<IEncrypter>();
            var jwtHandlerMock = new Mock<IJwtHandler>();

            Guid id = Guid.NewGuid();
            var email = "email@email.com";
            var password = "secret";
            var hashedPassword = "hash"; 
            var user = new User(id, email, "userName", hashedPassword, "user");
            var jwtDto = new JwtDto
            {
                Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9" +
                         ".eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ" +
                         ".SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c",
                Expires = 1516239022,
            };
            userRepositoryMock.Setup(x => x.GetAsync(user.Email)).ReturnsAsync(user);
            encrypterMock.Setup(x => x.GetSalt(password)).Returns("salt");
            encrypterMock.Setup(x => x.GetHash(password, "salt")).Returns(hashedPassword);
            jwtHandlerMock.Setup(x => x.CreateToken(id, user.Role)).Returns(jwtDto);
            var userService = new UserService(userRepositoryMock.Object, encrypterMock.Object, 
                mapperMock.Object, jwtHandlerMock.Object);
            //Act
            await userService.LoginAsync(user.Email, password);
            //Assert
            jwtHandlerMock.Verify(x => x.CreateToken(id, user.Role), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_ValidUserCredentials_ReturnCorrectTokenDto_()
        {
            //Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var encrypterMock = new Mock<IEncrypter>();
            var jwtHandlerMock = new Mock<IJwtHandler>();

            Guid id = Guid.NewGuid();
            var email = "email@email.com";
            var password = "secret";
            var hashedPassword = "hash";
            var user = new User(id, email, "userName", hashedPassword, "user");
            var jwtDto = new JwtDto
            {
                Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9" +
                         ".eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ" +
                         ".SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c",
                Expires = 1516239022,
            };
            userRepositoryMock.Setup(x => x.GetAsync(user.Email)).ReturnsAsync(user);
            encrypterMock.Setup(x => x.GetSalt(password)).Returns("salt");
            encrypterMock.Setup(x => x.GetHash(password, "salt")).Returns(hashedPassword);
            jwtHandlerMock.Setup(x => x.CreateToken(id, user.Role)).Returns(jwtDto);
            var userService = new UserService(userRepositoryMock.Object, encrypterMock.Object,
                mapperMock.Object, jwtHandlerMock.Object);
            //Act
            var testDto = await userService.LoginAsync(user.Email, password);
            //Assert
            testDto.Token.Should().BeEquivalentTo(jwtDto.Token);
            testDto.Expires.ToString().Should().BeEquivalentTo(jwtDto.Expires.ToString());
            testDto.Role.Should().BeEquivalentTo(user.Role);
        }
    }
}
