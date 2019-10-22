using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Passenger.Api;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace Passenger.Test.EndToEnd.Controllers
{
    public class UsersControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        public UsersControllerTests()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task given_valid_email_user_should_exist()
        {
            var email = "user1@email.com";
            var response = await _client.GetAsync($"users/{email}");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserDto>(responseString);

            Assert.Equal(email, user.Email);
        }
        [Fact]
        public async Task given_invalid_email_user_should_not_exist()
        {
            var email = "user100@email.com";
            var response = await _client.GetAsync($"users/{email}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            //Assert.Equal(email, user.Email);
        }
        [Fact]
        public async Task given_uqnique_email_user_should_be_created()
        {
            var request = new CreateUser
            {
                Email = "emailTest@email.com",
                UserName = "Test",
                Password = "secret"
            };

            var payload = GetPayload(request);
            var response = await _client.PostAsync("user", payload);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal($"users/{request.Email}", response.Headers.Location.ToString());
        }

        private static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
