using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Passenger.Tests.EndToEnd.Controllers
{
    public class UsersControllerTests : ControllerTestsBase
    {
        [Fact]
        public async Task given_invalid_email_user_should_not_exist()
        {
            var email = "user1000@email.com";
            var response = await Client.GetAsync($"users/{email}");
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.NotFound);
        }
    }
}
