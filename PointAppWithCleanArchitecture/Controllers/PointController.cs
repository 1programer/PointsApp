using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PointAppWithCleanArchitecture.Application.DTOS;
using PointAppWithCleanArchitecture.Interfaces;
using PointAppWithCleanArchitecture.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace Practice_8.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PointController : ControllerBase
    {
        private readonly IPointRepository _pointRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public PointController(IPointRepository pointRepository, ILogger<PointController> logger, IUserRepository userRepository, IMapper mapper)
        {
            _pointRepository = pointRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]

        public async Task<ActionResult<Point>> GetAll()
        {
            var books = await _pointRepository.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("GetByid/{id}")]

        public async Task<ActionResult<Point>> GetById(Guid id)
        {
            var point = await _pointRepository.GetByIdAsync(id);
            if (point == null)
                return NotFound();

            return Ok(point);
        }

        [HttpPost("Create")]

        public async Task<ActionResult> Create([FromBody] PointDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            var user = await _userRepository.GetByIdAsyncWithString(dto.UserId);
            if (user == null)
                return NotFound("User not found.");

            Point point = _mapper.Map<Point>(dto);


            await _pointRepository.CreateAsync(point);

            return CreatedAtAction(nameof(GetById), new { id = point.Id }, point);
        }

        [HttpPut("Update/{id}")]

        public async Task<ActionResult> Update(Guid id, [FromBody] PointDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            var existingPoint = await _pointRepository.GetByIdAsync(id);
            if (existingPoint == null)
                return NotFound();

            var genre = await _userRepository.GetByIdAsyncWithString(dto.UserId);
            if (genre == null)
                return BadRequest("Genre not found.");

            _mapper.Map(dto, existingPoint);

            await _pointRepository.UpdateAsync(id);

            return Ok();
        }


        [HttpDelete("Delete/{id}")]

        public async Task<ActionResult> Delete(Guid id)
        {
            await _pointRepository.DeleteAsync(id);
            return Ok();
        }
    }
}