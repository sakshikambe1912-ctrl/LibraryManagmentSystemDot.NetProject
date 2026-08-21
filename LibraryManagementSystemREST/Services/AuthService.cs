using LibraryManagmentSystem.Data;
using LibraryManagmentSystem.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryManagmentSystem.Services
{
    public class AuthService :IAuthService
    {
        private readonly AppDbContext context;
        private readonly IConfiguration configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public string? Login(string username, string password)//return JWT token or null if authentication fails
        {
            // Find user
            var user = context.User.FirstOrDefault(
                u => u.UserName == username && u.Password == password);

            // Invalid username/password
            if (user == null)
                return null;

            // Create claims -creation of jet
            var claim = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),//sdds a clsim contaning the unique user ID
        new Claim(ClaimTypes.Name, user.UserName),//adds a claim containing username
        new Claim(ClaimTypes.Role, user.Role),//adds a claim containing users assigned security role
    };

            // Get JWT key
            //retrieves a secret string from application , converts it into byte array nad build security key used for signing
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));

            // Credentials


            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            // Create JWT
            //Instatiates a new JWT configuration object
            //Issuer : set the token user(who made token)
            //Audience : set the token audience(who the token is intended for)
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claim,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

            // Convert token to string and return
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
