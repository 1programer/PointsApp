using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PointAppWithCleanArchitecture.Application.DTOS;
using PointAppWithCleanArchitecture.Domain.Models;
using PointAppWithCleanArchitecture.Interfaces;

namespace Practice_8.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;


        public UserController(IUserRepository userRepository, IMapper mapper, RoleManager<Role> userRoles, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _roleManager = userRoles;
            _userManager = userManager;
        }

        [HttpGet("GetAll")]
        [Authorize]

        public async Task<ActionResult<List<UserDto>>> GetAll()
        {
            var users = await _userRepository.GetAllAsync();
            List<User> pcodeUsers = new List<User>();
            var pcode = User.FindFirst("pcode")?.Value;
            foreach (var user in users)
            {
                if(user.PCode == pcode)
                {
                    pcodeUsers.Add(user);
                }
            }
            
            return _mapper.Map<List<UserDto>>(pcodeUsers);
        }

        [HttpGet("GetById/{id}")]

        public async Task<ActionResult<User>> GetById(string id)
        {
            var user = await _userRepository.GetByIdAsyncWithString(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost("Create")]

        public async Task<ActionResult> Create([FromBody] UserSignUpDto dto)
        {
            if (dto == null)
                return BadRequest();

            // Map DTO to User entity (without password)
            var user = _mapper.Map<User>(dto);

            // Create user with password using UserManager (this saves user and sets Id)
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                // Return errors if user creation failed
                return BadRequest(result.Errors);
            }

            // Ensure the "User" role exists
            if (!await _roleManager.RoleExistsAsync(Role.Customer))
            {
                await _roleManager.CreateAsync(new Role { Name = Role.Customer });
            }

            // Assign "User" role to the new user
            await _userManager.AddToRoleAsync(user, Role.Customer);

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }
        [HttpPut("Update/{id}")]

        public async Task<ActionResult> Update(string id, [FromBody] UserSignUpDto dto)
        {
            if (dto == null)
                return BadRequest();

            User User = _mapper.Map<User>(dto);

            var existingUser = await _userRepository.GetByIdAsyncWithString(id);
            if (existingUser == null)
                return NotFound();

            _mapper.Map(dto, existingUser);
            await _userRepository.UpdateAsyncWithString(id);
            return Ok();
        }

        [HttpDelete("Delete/{id}")]

        public async Task<ActionResult> Delete(string id)
        {
            await _userRepository.DeleteAsyncWithString(id);
            return Ok();
        }
        [HttpPost("UpdatePoints")]
        public async Task<ActionResult> UpdatePoints()
        {
            await _userRepository.UpdatePoints();
            return Ok();
        }
    }
}