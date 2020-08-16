using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Core;
using WebAPI.Core.Model;
using WebAPI.Service.User;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomerController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly JwtSettings _jwtSettings;

        public CustomerController(IUserServices userServices, IOptions<JwtSettings> jwtSettings)
        {
            _userServices = userServices;
            _jwtSettings = jwtSettings.Value;
        }

        
        [HttpPost("Login")]
        [AllowAnonymous]
        public ActionResult<UserWithToken> Login(UserModel model)
        {
            UserWithToken user = new UserWithToken();

            user = _userServices.UserVaild(model.EmailId, model.Password);

            if (user.IsValid)
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.FullName),
                        new Claim(ClaimTypes.Email, user.UserEmailId),
                        new Claim(ClaimTypes.Role, "Guest")
                    }),
                    Expires = DateTime.UtcNow.AddDays(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.AccessToken = tokenHandler.WriteToken(token);

                return Ok(user);
            }

            return NotFound(user);
        }



    }
}
