using Compliance_Platform.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Compliance_Platform.Model.CompPlatformLoginModel;

namespace Compliance_Platform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<CompPlatformUser> _signInManager;

        public AuthController(SignInManager<CompPlatformUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(
                model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);

            return Ok(new { succeeded = result.Succeeded });
        }
    }
}
