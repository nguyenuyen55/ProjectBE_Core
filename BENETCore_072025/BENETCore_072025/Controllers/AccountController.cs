using BENETCore_072025.DataAccess.DTO;
using BENETCore_072025.DataAccess.Services;
using BENETCore_072025.DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BENETCore_072025.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IunitOfWork _unitOfWork;
        private IAccountService _accountService;
        private IConfiguration _configuration;
        public AccountController(IunitOfWork iunitOfWork,IConfiguration configuration,IAccountService accountService)
        {
            _configuration = configuration;
            _unitOfWork = iunitOfWork;
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> AccountLogin(AccountLoginRequestData requestData)
        {
            var response = new ReturnResponData();
            try {
                var userLogin = await _accountService.UserLogin(requestData);
                if (userLogin == null)
                {
                    return BadRequest();
                }
               
                    var authClaims = new List<Claim> { 
                        new Claim(ClaimTypes.Name, userLogin.UserName), 
                        new Claim(ClaimTypes.NameIdentifier, userLogin.ID.ToString())};
            
                var newAccessToken = CreateToken(authClaims);
                var token = new JwtSecurityTokenHandler().WriteToken(newAccessToken);
                var refreshToken = GenerateRefreshToken();
                var expired = _configuration["JWT:RefreshTokenValidityInDays"] ?? "";


                var result_update = _accountService.AccountUpdateRefeshToken(new AccountUpdateRefeshTokenRequestData
                {
                    UserID = userLogin.ID,
                    RefeshToken = refreshToken,
                    RefeshTokenExpired = DateTime.Now.AddDays(Convert.ToInt32(expired))
                });

                response.ResponseCode = 1;
                response.ResponseMessage = "Đăng nhập thành công !";
                response.token = token;
                response.refeshToken = refreshToken;
                return Ok(response);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

    }
}
