using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Infrastructure.Exceptions
{
    public static class ErrorCodes
    {
        public static string EmailInUse => "email_in_use";
        public static string InvalidEmail => "invalid_email";
        public static string InvalidCredentials => "invalid_credentials";
        public static string UserNotFound => "user_not_found";
        public static string UserAlreadyExist => "user_adlrady_exist";
        public static string DriverNotFound => "driver_not_found";
        public static string DriverAlreadyExist => "driver_adlrady_exist";
    }
}
