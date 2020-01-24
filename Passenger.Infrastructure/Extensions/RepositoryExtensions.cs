using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ErrorCode = Passenger.Infrastructure.Exceptions.ErrorCodes;


namespace Passenger.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<User> GetUserOrFailAsync(this IUserRepository userRepository, Guid userId)
        {
            var user =  await userRepository.GetAsync(userId);
            if (user == null)
                throw new ServiceException(ErrorCode.UserNotFound, $"User with id: {userId} does not found");
            return user;
        }

        public static async Task<User> GetUserOrFailAsync(this IUserRepository userRepository, string email)
        {
            var user = await userRepository.GetAsync(email);
            if (user == null)
                throw new ServiceException(ErrorCode.UserNotFound, $"User with email: {email} does not found");
            return user;
        }

        public static async Task<Driver> GetDriverOrFailAsync(this IDriverRepository driverRepository, Guid userId)
        {
            var driver = await driverRepository.GetAsync(userId);
            if (driver == null)
                throw new ServiceException(ErrorCode.DriverNotFound, $"Driver with id: {userId} does not found");
            return driver;
        }
    }
}
