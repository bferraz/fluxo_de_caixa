using API.Core.Identity;
using API.Identidade.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Identidade.Services
{
    public class AuthenticationService
    {
        public readonly SignInManager<IdentityUser> SignInManager;
        public readonly UserManager<IdentityUser> UserManager;

        private readonly IAspNetUser _aspNetUser;
        private readonly IConfiguration _configuration;

        public AuthenticationService(SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager, 
            IAspNetUser aspNetUser,
            IConfiguration configuration)
        {
            SignInManager = signInManager;
            UserManager = userManager;
            _aspNetUser = aspNetUser;
            _configuration = configuration;
        }

        public async Task<UserResponseLoginModel> GenerateJwt(string email)
        {
            var user = await UserManager.FindByEmailAsync(email);
            var claims = await UserManager.GetClaimsAsync(user);

            var identityClaims = await GetUserClaims(claims, user);
            var encodedToken = EncodeToken(identityClaims);

            return GetTokenResponse(encodedToken, user, claims);
        }

        private async Task<ClaimsIdentity> GetUserClaims(ICollection<Claim> claims, IdentityUser user)
        {
            var userRoles = await UserManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private string EncodeToken(ClaimsIdentity claimsIdentity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Secret").Value);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _configuration.GetSection("AppSettings:Emissor").Value,
                Audience = _configuration.GetSection("AppSettings:ValidoEm").Value,
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddHours(int.Parse(_configuration.GetSection("AppSettings:ExpiracaoHoras").Value)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private UserResponseLoginModel GetTokenResponse(string encodedToken, IdentityUser user, IEnumerable<Claim> claims)
        {
            return new UserResponseLoginModel
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(int.Parse(_configuration.GetSection("AppSettings:ExpiracaoHoras").Value)).TotalSeconds                
            };
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
