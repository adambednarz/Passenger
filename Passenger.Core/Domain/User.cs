using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace Passenger.Core.Domain
{
    public class User
    {
        private static readonly Regex _nameRegex = new Regex("^(?=.{ 8,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$");

        public Guid Id { get; protected set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string UserName { get; protected set; }
        public string FullName { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected User()
        {
        }

        public User(string email, string username, 
            string password, string salt)
        {
            Id = Guid.NewGuid();
            SetEmail(email);
            SetUserName(username);
            SetPassword(password);
            SetSalt(salt);
            CreatedAt = DateTime.UtcNow;
        }
        public void SetUserName(string userName)
        {
            //if(!_nameRegex.IsMatch(userName))
            //{
            //    throw new Exception("Username is invalid");
            //    //throw new DomainException(ErrorCodes.InvalidUsername,
            //    //    "Username is invalid.");
            //}
            if(string.IsNullOrEmpty(userName))
            {
                throw new Exception("Username is invalid");
            }
            if (UserName == userName)
            {
                return;
            }
            UserName = userName;
        }

        public void SetEmail(string email)
        {
            if(string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("Email is invalid");
            }
            if(Email == email)
            {
                return;
            }
            Email = email.ToLowerInvariant();

        }
        public void SetPassword(string password)
        {
            if(string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Password can not be  empty");
            }
            if(password.Length < 4)
            {
                throw new Exception("Password must contain at least 4 characters.");
            }
            if (password.Length > 100)
            {
                throw new Exception("Password can not contain mor than 100 characters.");
            }
            if (Password == password)
            {
                return;
            }
            Password = password;
        }
        public void SetSalt(string salt)
        {
            if(string.IsNullOrEmpty(salt))
            {
                throw new Exception("Salt can not be empty.");
            }
            if(Salt == salt)
            {
                return;
            }
            Salt = salt;
        }

    }
}
