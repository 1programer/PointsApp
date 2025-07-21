using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PointAppWithCleanArchitecture.Application.DTOS;
using PointAppWithCleanArchitecture.Domain.Models;
using PointAppWithCleanArchitecture.Interfaces;

namespace Practice_8.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;

        }

        [HttpGet("GetAll")]

        public async Task<ActionResult<List<UserDto>>> GetAll()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<List<UserDto>>(users);
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
            User User = _mapper.Map<User>(dto);


            await _userRepository.CreateAsync(User); 
            
            return CreatedAtAction(nameof(GetById), new { id = User.Id }, User);
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