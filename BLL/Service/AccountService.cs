using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BLL.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using DAL.Entities;
using DAL.DatabaseContext;


namespace BLL.Service
{
    public class AccountService : IAccountService
    {
        private readonly ShouraiDB _context;
        private readonly IConfiguration _config;

        public AccountService(ShouraiDB context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<string> Register(string username, string password)
        {
            if (await _context.Accounts.AnyAsync(a => a.Username == username))
                throw new Exception("Username already exists.");

            var newUser = new Account
            {
                Username = username,
                Password = password,
                ScreenName = "Level1",
                Strength = 0,
                Agility = 0,
                Intelligence = 0,
                Vitality = 0,
                Gold = 0
            };

            _context.Accounts.Add(newUser);
            await _context.SaveChangesAsync();
            return "User registered successfully";
        }


        public async Task<string> Login(string username, string password)
        {
            var user = await _context.Accounts.FirstOrDefaultAsync(a => a.Username == username);
            if (user == null)
                throw new Exception("Invalid username or password");

            if (password != user.Password)
                throw new Exception("Invalid username or password");

            return GenerateJwtToken(user);
        }


        private string GenerateJwtToken(Account user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim("username", user.Username)
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
