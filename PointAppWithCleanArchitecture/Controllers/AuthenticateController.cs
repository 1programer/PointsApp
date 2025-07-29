using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PointAppWithCleanArchitecture.Application.DTOS;
using PointAppWithCleanArchitecture.Application.Interfaces;
using PointAppWithCleanArchitecture.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;

namespace JWTAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthenticateController(UserManager<User> userManager, RoleManager<Role> roleManager, IConfiguration configuration, IJwtTokenGenerator jwtTokenGenerator )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _jwtTokenGenerator = jwtTokenGenerator;

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized("Invalid username or password");

            var token = _jwtTokenGenerator.GenerateToken(user);

            return Ok(new
            {
                token,
                expiration = DateTime.UtcNow.AddMinutes(60)
            });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserSignUpDto model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            User user = new User()
            {
                UserName = model.UserName,
                Email = model.Email,
                Name = model.Name,
                LName = model.LName,
                Phone = model.Phone,
                PCode = model.PCode,
                PNumber = model.PNumber,
                BirthDate = model.BirthDate,
                Points = 0
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(Role.User))
                await _roleManager.CreateAsync(new Role { Name = Role.User });

            if (await _roleManager.RoleExistsAsync(Role.User))
            {
                await _userManager.AddToRoleAsync(user, Role.User);
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("register-admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] UserSignUpDto model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            User user = new User()
            {
                UserName = model.UserName,
                Email = model.Email,
                Name = model.Name,
                LName = model.LName,
                Phone = model.Phone,
                PCode = model.PCode,
                PNumber = model.PNumber,
                BirthDate = model.BirthDate,
                Points = 0
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(Role.Admin))
                await _roleManager.CreateAsync(new Role { Name = Role.Admin });


            if (await _roleManager.RoleExistsAsync(Role.Admin))
            {
                await _userManager.AddToRoleAsync(user, Role.Admin);
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }
    }
}