using Passenger.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public interface IUserService : IService
    {
        Task<UserDto> GetAsync(string email); 
        Task RegisterAsync(Guid id, string email, string userName, string password, string role);
        Task<TokenDto> LoginAsync(string email, string password);
    }
}
