using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Act_5.Properties.Model
{
    public class AdminModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime CreatedDate { get; set; }

        // Constructor for creating a new admin account
        public AdminModel(string username, string password, string email, string role)
        {
            Username = username;
            Password = password;
            Email = email;
            Role = role;
            CreatedDate = DateTime.Now;
        }

        // Login method to validate credentials and return JWT if successful
        public string Login(string username, string password)
        {
            if (Username == username && Password == password)
            {
                // Generate JWT if login is successful
                return GenerateJwtToken();
            }
            else
            {
                return null; // Return null if login fails
            }
        }

        // Method to generate JWT token
        private string GenerateJwtToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKeyHere")); // Replace with a secure key
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, Username),
                new Claim("role", Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: DateTime.Now.AddHours(1), // Token valid for 1 hour
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
