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
        public string UserName { get; protected set; }
        public string FullName { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public string UserName { get; protected set; }
        public string FullName{ get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected User()
        {
        }

        public User(string email, string username, 
            string password)
        {
            Id = Guid.NewGuid();
            SetUsername(username);
            SetEmail(email);
            SetPassword(password);
            CreatedAt = DateTime.UtcNow;
        }

        public void SetUsername(string userName)
        {
            if(string.IsNullOrEmpty(userName))
            {
                throw new Exception("User name can't be empty.");
            }
            if(UserName == userName)
            {
                return;
            }
            UserName = userName;
        }

        public void SetEmail(string email)
        {
            if(string.IsNullOrEmpty(email))
            {
                throw new Exception("Email can not be empty.");
            }
            if(Email == email)
            {
                return;
            }
            Email = email.ToLowerInvariant();
        }
        public void SetPassword(string password)
        {
            if(string.IsNullOrEmpty(password))
            {
                throw new Exception("Password can not be empty");
            }
            if(password.Length < 4)
            {
                throw new Exception("Password must be longer than 4 signs.");
            }
            if(password.Length > 100)
            {
                throw new Exception("Password can not be longer than 100 signs.");
            }
            if(Password == password)
            {
                return;
            }
            Password = password;
        }
        public void SetSalt(string salt)
        {
            if (string.IsNullOrEmpty(salt))
            {
                throw new Exception("Salt can not be empty");
            }
            if (salt.Length < 4)
            {
                throw new Exception("Salt must be longer than 4 signs.");
            }
            if (salt.Length > 10)
            {
                throw new Exception("Salt can not be longer than 10 signs.");
            }
            if (Salt == salt)
            {
                return;
            }
            Salt = salt;
        }

    }
}
