using AuthenticationApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationApi.Services.Implementation
{
    public class AuthenticationService: IAuthenticationService
    {

            private readonly UserManager<IdentityUser> _userManager;
            private IConfiguration _config;

            public AuthenticationService(UserManager<IdentityUser> userManager,
                IConfiguration config)
            {
                _userManager = userManager;
                _config = config;
            }
            public async Task<bool> RegisterUser(RegisterModel model)
            {
                var identityUser = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await _userManager.CreateAsync(identityUser, model.Password);
                return result.Succeeded;
            }

            public async Task<bool> Login(LoginModel model)
            {
                var identityUser = await _userManager.FindByEmailAsync(model.Email);

                if (identityUser is null) return false;
                model.Id = identityUser.Id;

                return await _userManager.CheckPasswordAsync(identityUser, model.Password);
            }

            public string GenerateTokenString(LoginModel model)
            {
                IEnumerable<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,model.Email),
                new Claim(ClaimTypes.Role,"Admin"),
            };

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));

                SigningCredentials signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

                var securityToken = new JwtSecurityToken(
                         claims: claims,
                         expires: DateTime.Now.AddMinutes(60),
                     issuer: _config.GetSection("Jwt:Issuer").Value,
                     audience: _config.GetSection("Jwt:Audience").Value,
                     signingCredentials: signingCred);


                string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
                return tokenString;
            }
        }
    }
