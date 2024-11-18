using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Act_5.Properties.Model;

namespace Act_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        // Define a secret key for JWT token generation (should be stored securely in production)
        private readonly string _jwtSecretKey = "YourSecretKeyHere"; // Replace with a strong key
        private readonly string _issuer = "yourdomain.com"; // Define your issuer
        private readonly string _audience = "yourdomain.com"; // Define your audience

        [HttpPost("login")]
        public IActionResult Login([FromBody] AdminModel loginRequest)
        {
            // In a real application, you would verify these credentials against a database
            var validAdmin = new AdminModel("adminUser", "securePassword123", "admin@example.com", "SuperAdmin");

            // Check if credentials are correct
            if (loginRequest.Username == validAdmin.Username && loginRequest.Password == validAdmin.Password)
            {
                // Generate JWT token
                var token = GenerateJwtToken(validAdmin);
                return Ok(new { token });
            }
            else
            {
                return Unauthorized("Invalid username or password");
            }
        }

        private string GenerateJwtToken(AdminModel admin)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, admin.Username),
                new Claim("role", admin.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
