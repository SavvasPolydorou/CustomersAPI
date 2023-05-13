using CustomersAPI.Interfaces;
using CustomersAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationAndAuthorization.Controllers
{

    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        //should not be stored here for security reasons, but it's ok for this assessment
        private const string tokenSecret = "JWTKeyShouldNotBeStoredHere";
        //way too long for a bearer token, but for the purpose of this assessment I'll leave it at 10 hours
        private static readonly TimeSpan tokenLifeTime = TimeSpan.FromHours(10);
        private ICustomerService _customerService;
        private IConfiguration _configuration;

        public AuthenticateController(ICustomerService customerService, IConfiguration configuration)
        {
            _customerService = customerService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Authorize")]
        public async Task<IActionResult> Authorize([FromBody] AuthenticationRequest request)
        {
           var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(tokenSecret);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub, request.UserName),
            };


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(tokenLifeTime),
                Issuer = "JWTAuthenticationServer",
                Audience = "JWTServicePostmanClient",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);
            return Ok(jwt);
        }



    }
}
