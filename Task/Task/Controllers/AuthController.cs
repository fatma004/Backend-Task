using BL.Dto;
using BL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Tebnabawe.Web
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthController(IHttpContextAccessor httpContextAccessor, IAuthService authService, IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _authService = authService;
            _userManager = userManager;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

        }
        [HttpPost("register/{Role}")]
        public async Task<IActionResult> RegisterAsync(RegisterModel model, string Role = "User")
        {

            var result = await _authService.RegisterAsync(model, Role);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {

            var result = await _authService.LoginAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }
        
    }
}
