using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthServiceDomain.DTO;
using AuthServiceDomain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthServiceApp.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;

        public AuthController(IMapper mapper, IConfiguration config, 
            UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
        {
            _mapper = mapper;
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] AuthRegisterDto registerDetail)
        {
            var userDetail = _mapper.Map<UserModel>(registerDetail);
            var createResult = await _userManager.CreateAsync(userDetail, registerDetail.Password);
            if (!createResult.Succeeded) return BadRequest();
            var respondUserDetail = _mapper.Map<AuthRegisterDto>(userDetail);
            return CreatedAtRoute(nameof(GetUser), new { userId = userDetail.Id }, respondUserDetail);
        }

        [HttpGet("user/{userId}", Name = nameof(GetUser))]
        public async Task<ActionResult> GetUser(int userId)
        {
            var userDetail = await _userManager.FindByIdAsync(userId.ToString());
            if (userDetail is null) return BadRequest("User not found");
            var userDetailDto = _mapper.Map<AuthMemberDetailDto>(userDetail);
            return Ok(new { user = userDetailDto });
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] AuthLoginDtos loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            var userResult = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!userResult) return Unauthorized();
            var memberDetailObj = _mapper.Map<AuthMemberDetailDto>(user);

            return Ok(new
            {
                token = GenerateJwtToken(memberDetailObj),
                user = memberDetailObj
            });
        }

        private string GenerateJwtToken(AuthMemberDetailDto memberDetailObj)
        {
            // Create Claims for payload
            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier, memberDetailObj.Id.ToString()),
                new Claim(ClaimTypes.Name, memberDetailObj.UserName)
            };

            // Create Key for credential
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:TokenKey").Value));
            // Create Credential by initializing with key and Sha256 algorithms
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            // Create TokenDecriptor by initializing with subject, Expire, SigningCredential
            var tokenDecriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(int.Parse(_config.GetSection("AppSettings:TokenExpire").Value)),
                SigningCredentials = credential
            };

            // Create Jwt Token handler
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDecriptor);

            return tokenHandler.WriteToken(securityToken);
        }
    }
}
