using CVExpress.API.Constants;
using CVExpress.API.Models.Authentication;
using CVExpress.API.Models.Settings;
using CVExpress.Entities.Efos;
using CVExpress.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CVExpress.API.Controllers
{
    #region Authentication Controllers

    [ApiExplorerSettings(GroupName = ApiConstants.DocumentationGroupAuthentication)]
    public class AuthenticationController : ControllerBase
    {
        private readonly CVExpressDbContext _context;
        private readonly JwtSettings _jwtSettings;

        public AuthenticationController(CVExpressDbContext context, JwtSettings jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings;
        }

        #region UserLogin

        [AllowAnonymous]
        [HttpPost("/userlogin")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(LoginUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LoginUser([FromBody, Required] LoginUserRequest loginUserRequest)
        {
            if (loginUserRequest == null)
            {
                return BadRequest("Email e Password são obrigatórios.");
            }

            RegisterUsersEfo? user = await _context.RegisterUsers.FirstOrDefaultAsync(
                u => u.Email == loginUserRequest.Email && u.Password == loginUserRequest.Password);

            if (user == null)
            {
                return Unauthorized("Dados inválidos.");
            }

            var userData = new
            {
                user.Id,
                user.FullName,
                user.BirtDate,
                user.Location,
                user.Country,
                user.Nationality,
                user.Email,
                user.PhoneNumber,
                user.Password,
            };

            var userIssuer = _jwtSettings.Issuer;
            var userAudience = _jwtSettings.Audience;
            var userKey = Encoding.ASCII.GetBytes(_jwtSettings.Key);
            var userTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim("Id", userData.Id.ToString()),
            new Claim(ClaimTypes.Role, "User"),
            new Claim(JwtRegisteredClaimNames.Sub, userData.Email),
            new Claim("FullName", userData.FullName),
            new Claim("BirtDate", userData.BirtDate.ToString()),
            new Claim("Location", userData.Location),
            new Claim("Country", userData.Country),
            new Claim("Nationality", userData.Nationality),
            new Claim("PhoneNumber", userData.PhoneNumber.ToString()),
            new Claim("Password", userData.Password),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        }),
                Issuer = userIssuer,
                Audience = userAudience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(userKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var userTokenHandler = new JwtSecurityTokenHandler();
            var userToken = userTokenHandler.CreateToken(userTokenDescriptor);
            var userJwtToken = userTokenHandler.WriteToken(userToken);

            return Ok(new LoginUserResponse(userJwtToken));
        }

        #endregion

        #region LoginAdmin

        [AllowAnonymous]
        [HttpPost("/adminlogin")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(LoginAdminResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LoginAdmin([FromBody, Required] LoginAdminRequest loginRequest)
        {
            if (loginRequest.UserName == "RuiBarreto" && loginRequest.Password == "Rui@Barreto-123")
            {
                var adminIssuer = _jwtSettings.Issuer;
                var adminAudience = _jwtSettings.Audience;
                var adminKey = Encoding.ASCII.GetBytes(_jwtSettings.Key);
                var adminTokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("Id", Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Role, "Admin"),
                        new Claim(JwtRegisteredClaimNames.Sub, loginRequest.UserName),
                        new Claim(JwtRegisteredClaimNames.Email, "a@a.pt"),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    }),
                    Issuer = adminIssuer,
                    Audience = adminAudience,
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(adminKey),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var adminTokenHandler = new JwtSecurityTokenHandler();
                var adminToken = adminTokenHandler.CreateToken(adminTokenDescriptor);
                var adminJwtToken = adminTokenHandler.WriteToken(adminToken);

                return Ok(new LoginAdminResponse(adminJwtToken));
            }
            return Unauthorized();
        }

        #endregion
    }

    #endregion
}
