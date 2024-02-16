using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Core.Entities;
using Cards.DTOs.AccountsDtos;
using Newtonsoft.Json;

namespace Cards.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AccountsController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ILogger<AccountsController> _logger;
        private readonly UserManager<SystemUser> _userManager;
        private readonly SignInManager<SystemUser> _signInManager;

        public AccountsController(
            UserManager<SystemUser> userManager,
            ITokenService tokenService,
            SignInManager<SystemUser> signInManager,
            ILogger<AccountsController> logger)
        {
            _logger = logger;
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(loginDto.UserName);

                if (user == null) return BadRequest("Invalid login credentials");

                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

                if (!result.Succeeded) return Unauthorized("Invalid login credentials");

                var roles = await _userManager.GetRolesAsync(user);

                var tokenResult = _tokenService.CreateToken(user, roles);

                var tokenResponse = new TokenResponse
                {
                    Token = tokenResult.token,
                    ExpiresIn = tokenResult.expiresin
                };

                return Ok(tokenResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(JsonConvert.SerializeObject(ex));

                return StatusCode(500);
            }
        }
    }
}