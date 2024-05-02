using Cinema.Core.Domain.IdentityEntities;
using Cinema.Core.DTO;
using Cinema.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.Controllers
{
    [AllowAnonymous]
    public class AccountController : CustomControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwtService;

        public AccountController(
            UserManager<ApplicationUser> userManager, 
            RoleManager<ApplicationRole> roleManager, 
            SignInManager<ApplicationUser> signInManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApplicationUser>> PostRegister(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(entry => entry.Errors)
                    .Select(error => error.ErrorMessage);

                string errorMessage = string.Join('|', errors);

                return Problem(errorMessage);
            }

            ApplicationUser user = new ApplicationUser
            {
                UserName = registerDTO.Username,
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                var authenticationResponse = _jwtService.CreateJwtToken(user);

                return Ok(authenticationResponse);
            }
            else
            {
                var errors = result.Errors.Select(temp => temp.Description);

                string errorMessage = string.Join('|', errors);

                return Problem(errorMessage);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApplicationUser>>PostLogin(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(entry => entry.Errors)
                    .Select(error => error.ErrorMessage);

                string errorMessage = string.Join('|', errors);

                return Problem(errorMessage);
            }

            var result = await _signInManager.PasswordSignInAsync(loginDTO.EmailOrUsername, loginDTO.Password, isPersistent: false,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(loginDTO.EmailOrUsername)  ?? await _userManager.FindByEmailAsync(loginDTO.EmailOrUsername);

                if (user is null)
                {
                    return NoContent();
                }

                var authenticationResponse = _jwtService.CreateJwtToken(user);

                return Ok(authenticationResponse);
            }
            else
            {
                return Problem("Invalid Email/Username or Password");
            }
        }

        [HttpGet("logout")]
        public async Task<ActionResult<ApplicationUser>> GetLogout()
        {
            await _signInManager.SignOutAsync();
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
        {
            if (await _userManager.FindByEmailAsync(email) is null)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }
    }
}
