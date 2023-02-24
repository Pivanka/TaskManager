using BLL.Dtos;
using BLL.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register([FromBody] RegisterDto model)
        {
            var result = await _userService.RegisterUserAsync(model);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDto model)
        {
            var result = await _userService.LoginUserAsync(model);

            return Ok(result);
        }
    }
}
