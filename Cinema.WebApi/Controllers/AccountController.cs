using System.Security.Claims;
using System.Security.Cryptography;
using Cinema.Core.Domain.IdentityEntities;
using Cinema.Core.DTO;
using Cinema.Core.Enums;
using Cinema.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using NuGet.Common;

namespace Cinema.WebApi.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseController
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
                Role = registerDTO.Role
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (result.Succeeded)
            {
                await CreateUserRole(registerDTO, user);

                var authenticationResponse = _jwtService.CreateJwtToken(user);

                user.RefreshToken = authenticationResponse.RefreshToken;
                user.RefreshTokenExpiration = authenticationResponse.RefreshTokenExpiration;
                await _userManager.UpdateAsync(user);

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

            var user = await _userManager.FindByNameAsync(loginDTO.EmailOrUsername) ?? await _userManager.FindByEmailAsync(loginDTO.EmailOrUsername);

            if (user is null)
            {
                return Problem("Invalid Email/Username");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var authenticationResponse = _jwtService.CreateJwtToken(user);

                user.RefreshToken = authenticationResponse.RefreshToken;
                user.RefreshTokenExpiration = authenticationResponse.RefreshTokenExpiration;
                await _userManager.UpdateAsync(user);

                return Ok(authenticationResponse);
            }
            else
            {
                return Problem("Invalid Password");
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

        [HttpPost("generate-new-jwt-token")]
        public async Task<IActionResult> GenerateNewAccessToken(TokenModel tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            ClaimsPrincipal? principal = _jwtService.GetPrincipalFromJwtToken(tokenModel.Token);

            if (principal is null)
            {
                return BadRequest("Invalid jwt access token");
            }

            string? email = principal.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            if (user is null 
                || user.RefreshToken != tokenModel.RefreshToken
                || user.RefreshTokenExpiration  <= DateTime.Now)
            {
                return BadRequest("Invalid refresh token");
            }

            var authenticationResponse = _jwtService.CreateJwtToken(user);

            user.RefreshToken = authenticationResponse.RefreshToken;
            user.RefreshTokenExpiration = authenticationResponse.RefreshTokenExpiration;
            await _userManager.UpdateAsync(user);

            return Ok(authenticationResponse);
        }

        private async Task CreateUserRole(RegisterDTO registerDTO, ApplicationUser user)
        {

            switch (registerDTO.Role)
            {
                case UserRoleOptions.Admin:
                {
                    if (await _roleManager.FindByNameAsync(UserRoleOptions.Admin.ToString()) is null)
                    {
                        var applicationRole = new ApplicationRole()
                        {
                            Name = UserRoleOptions.Admin.ToString(),
                        };
                        await _roleManager.CreateAsync(applicationRole);
                    }

                    await _userManager.AddToRoleAsync(user, UserRoleOptions.Admin.ToString());
                    break;
                }
                case UserRoleOptions.User:
                {
                    if (await _roleManager.FindByNameAsync(UserRoleOptions.User.ToString()) is null)
                    {
                        var applicationRole = new ApplicationRole()
                        {
                            Name = UserRoleOptions.User.ToString(),
                        };
                        await _roleManager.CreateAsync(applicationRole);
                    }

                    await _userManager.AddToRoleAsync(user, UserRoleOptions.User.ToString());
                    break;
                }
            }
        }
    }
}
