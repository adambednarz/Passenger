using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Infrastructure.Commands.Accounts
{
    public class Login : ICommand
    {
        public Guid TokenId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
