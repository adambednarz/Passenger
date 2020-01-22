﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Extensions;
using Passenger.Infrastructure.Services.Interfaces;
using Passenger.Infrastructure.Settings;

namespace Passenger.Infrastructure.Services
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSettings _settings;
        public JwtHandler(JwtSettings settings)
        {
            _settings = settings;
        }

        public JwtDto CreateToken(string email, string role)
        {
            var now = DateTime.UtcNow;
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),                      // dla kogo 
                new Claim(ClaimTypes.Role, role),                                   // jaka jest jego rola (uprawnienia)
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),  // unikalny identyfikator tokena
                new Claim(JwtRegisteredClaimNames.Iat, now.ToTimeStamp().ToString(), ClaimValueTypes.Integer64)
                //IssueAt data stworzenia -- musi być w formacie UNIX od 01.01.1970
            };

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key)),
                SecurityAlgorithms.HmacSha256);

            var expires = now.AddMinutes(_settings.ExpiryMinutes);
            var jwt = new JwtSecurityToken(
                issuer: _settings.Issuer,
                claims: claims,
                notBefore: now,                 // ważnu od teraz
                expires: expires,
                signingCredentials: signingCredentials
                );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtDto
            {
                Token = token,
                Expiries = expires.ToTimeStamp()
            };
        }
    }
}
