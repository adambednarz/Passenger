using Passenger.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services.Interfaces
{
    public interface IUserService : IService
    {
        Task<IEnumerable<UserDto>> BrowseAsync();
        Task<UserDto> GetAsync(string email);
        Task RegisterAsync(Guid userId, string email,
            string userName, string role, string password);
        Task LoginAsync(string email, string password);
    }
}
