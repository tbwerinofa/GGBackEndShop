using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthenticationManager
{
    public class JwtTokenHandler
    {
        #region Fields
        public const string JWT_TOKEN_KEY = "D720DE70-E621-477F-91F4-47733458EA70";
        private const int JWT_TOKEN_VALIDITY_Hours =20;
        #endregion

        #region CTOR
        public JwtTokenHandler()
        {
        }
        #endregion


        #region Method
        /// <summary>
        /// Generate JWT Token
        /// </summary>
        /// <param name="email">Principal Email</param>
        /// <param name="Id">Principal Id</param>
        /// <returns></returns>
        public AuthenticationResponse? GenerateJwtToken(string email,string Id)
            {

            var tokenExpiryTimeStamp = DateTime.Now.AddHours(JWT_TOKEN_VALIDITY_Hours);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_TOKEN_KEY);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name,email),
                new Claim(JwtRegisteredClaimNames.Sid,Id),
                new Claim(ClaimTypes.NameIdentifier,Id)
            });

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires =tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new AuthenticationResponse
            {
                UserName = email,
                ExpiresIn =(int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                JwtToken =token
            };
        }
        #endregion
    }
}
