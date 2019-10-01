
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Core.Basics.IServices;
using Core.Basics.Models;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Core.Basics.WebAPI.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private static readonly string Secret = Guid.NewGuid().ToString();
        public static byte[] Key => Encoding.ASCII.GetBytes(Secret);

        private List<User> _users = new List<User> {
            new User {Id = 1, Username = "username", Password = "Pa$$w0rd"}
        };

        private readonly ILogger _logger;
        public AuthenticateService(ILogger<AuthenticateService> logger) {
            _logger = logger;
        }
        public string Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);
            if(user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new Claim[] {new Claim(ClaimTypes.Name, user.Id.ToString())}),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

}