using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using KSManager_API.DB;
using KSManager_API.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KSManager_API.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly KsDatabase _database;
        private readonly IConfiguration _configuration;

        public LoginController(KsDatabase database, IConfiguration configuration)
        {
            _database = database;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login(string username, string password)
        {
            var user = _database.User.SingleOrDefault(x => x.Username == username);
            if (user != null)
            {
                using (var pbkdf2 = new Rfc2898DeriveBytes(password, user.Salt, user.Iterations, HashAlgorithmName.SHA512))
                {
                    var bytes = pbkdf2.GetBytes(64);

                    if (bytes.SequenceEqual(user.Password))
                    {
                        var key = new SymmetricSecurityKey(_configuration.GetSection("JWT")["Secret"].ToHex());
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

                        var claims = new[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim("jit", Guid.NewGuid().ToString())
                        };

                        var token = new JwtSecurityToken("KSManager API", "KSManager", claims,
                            expires: DateTime.Now.AddDays(7), signingCredentials: creds);

                        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                        return Ok(new
                        {
                            accessToken = tokenString
                        });
                    }
                }
            }

            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterObject registerObject)
        {
            var salt = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            var user = new User()
            {
                Username = registerObject.Username,
                FirstName = registerObject.FirstName,
                LastName = registerObject.LastName,
                Birthday = registerObject.Birthday,
                Salt = salt
            };

            using (var rfc = new Rfc2898DeriveBytes(registerObject.Password, salt, 50000, HashAlgorithmName.SHA512))
            {
                user.Password = rfc.GetBytes(64);
            }

            _database.User.Add(user);
            await _database.SaveChangesAsync();
            return Ok();
        }

        public class RegisterObject
        {
            public string Username { get; set; }

            public string Password { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public DateTime? Birthday { get; set; }
        }
    }
}
