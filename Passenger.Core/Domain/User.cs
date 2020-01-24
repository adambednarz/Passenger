using Passenger.Core.Exceptions;
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
        private static readonly List<string> _roles = new List<string> { "superadmin", "admin", "user" };

        public Guid Id { get; protected set; }
        public string UserName { get; protected set; }
        public string FullName { get; protected set; }
        public string Email { get; protected set; }
        public string Role { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }


        protected User()
        {
        }

        public User(Guid userId, string email, string username,
            string role, string password, string salt)
        {
            Id = userId;
            SetUsername(username);
            SetEmail(email);
            SetRole(role);
            SetPassword(password);
            SetSalt(salt);
            CreatedAt = DateTime.UtcNow;
        }

        public void SetUsername(string userName)
        {
            if(string.IsNullOrEmpty(userName))
            {
                throw new DomainException(ErrorCodes.InvalidEmail, "Username is invalid.");
            }
            if(UserName == userName)
            {
                return;
            }
            UserName = userName.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetEmail(string email)
        {
            if(string.IsNullOrEmpty(email))
            {
                throw new DomainException(ErrorCodes.InvalidEmail,"Email can not be empty.");
            }
            if(Email == email)
            {
                return;
            }
            Email = email.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetRole(string role)
        {
            if (!_roles.Contains(role.ToLowerInvariant()))
                throw new DomainException(ErrorCodes.InvalidRole, $"Role: '{role}' is not correct.");
            Role = role.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string password)
        {
            if(string.IsNullOrEmpty(password))
            {
                throw new DomainException(ErrorCodes.InvalidPassword,  "Password can not be empty");
            }
            if(password.Length < 4)
            {
                throw new DomainException(ErrorCodes.InvalidPassword, "Password must be longer than 4 signs.");
            }
            if(Password == password)
            {
                return;
            }
            Password = password;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetSalt(string salt)
        {
            if (string.IsNullOrEmpty(salt))
            {
                throw new DomainException(ErrorCodes.InvalidPassword, "Salt can not be empty");
            }
            if (Salt == salt)
            {
                return;
            }
            Salt = salt;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
