﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Core.Domain
{
    public class User
    {
        public Guid Id { get; protected set; }
        public string UserName { get; protected set; }
        public string FullName { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Role { get; protected set; }
        public string Salt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected User()
        {
        }

        public User(Guid id, string email, string username, 
            string password, string role)
        {
            Id =id;
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

        public void SetRole(string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                throw new Exception("role can not be empty.");
            }
            if (Role == role)
            {
                return;
            }
            Role = role.ToLowerInvariant();
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
