using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Services;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Passenger.Tests.EndToEnd.Controllers
{
    public class DriversControllerTests : ControllerTestsBase
    {
   

        [Fact]
        public async Task given_valid_driver_and_new_password_it_should_be_changed()
        {
            var user = await GetUserAsync("user1@email.com");
            var userId = user.Id;
            var command = new CreateDriver
            {
                UserId = userId,
                VehicleBrand = "Ford",
                VehicleName = "Fiesta",
            };

            var payload = GetPayload(command); 

            var response = await Client.PostAsync("drivers", payload);
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Created);
        }


    private async Task<UserDto> GetUserAsync(string email)
    {
        var response = await Client.GetAsync($"users/{email}");
        var responseString = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<UserDto>(responseString);
    }
}
}
