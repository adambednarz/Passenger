using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Passenger.Infrastructure.Commands.Users
{
    public class CreateUser : ICommand
    {
        public string Email {get; set; }
        [Required]
        public string Password { get; set; }
        public string  UserName { get; set; }
        public string Salt{ get; set; }
    }
}
