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
    [Authorize]
     public class ItemController : ControllerBase
    {
        private readonly IItemRepository _ItemRepository;
        private readonly IPointRepository _PointRepository;
        private readonly IMapper _mapper;



        public ItemController(IItemRepository ItemRepository, ILogger<ItemController> logger, IMapper mapper, IPointRepository pointRepository)
        {
            _ItemRepository = ItemRepository;
            _mapper = mapper;
            _PointRepository = pointRepository;
        }


        [HttpGet("GetAll")]

        public async Task<ActionResult<IEnumerable<Item>>> GetAll()
        {
            var Items = await _ItemRepository.GetAllAsync();
            return Ok(Items);
        }

        [HttpGet("GetById/{id}")]

        public async Task<ActionResult<Item>> GetById(Guid id)
        {
            var Item = await _ItemRepository.GetByIdAsync(id);
            if (Item == null)
                return NotFound();

            return Ok(Item);
        }

        [HttpPost("Create")]

        public async Task<ActionResult> Create([FromBody] ItemDto dto)
        {
            if (dto == null)
                return BadRequest();
            Item item = _mapper.Map<Item>(dto);


            await _ItemRepository.CreateAsync(item); 
            
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut("Update/{id}")]

        public async Task<ActionResult> Update(Guid id, [FromBody] ItemDto dto)
        {
            if (dto == null)
                return BadRequest();

            Item item = _mapper.Map<Item>(dto);

            var existingItem = await _ItemRepository.GetByIdAsync(id);
            if (existingItem == null)
                return NotFound();

            _mapper.Map(dto, existingItem);
            await _ItemRepository.UpdateAsync(id);
            return Ok();
        }

        [HttpDelete("Delete/{id}")]

        public async Task<ActionResult> Delete(Guid id)
        {
            await _ItemRepository.DeleteAsync(id);
            return Ok();
        }
        [HttpPost("BuyItem/{id}/{userid}")]

        public async Task<ActionResult> BuyItem(Guid id, string userid, decimal quantity)
        {
            IEnumerable<Point> points = await _PointRepository.GetAllAsync();
            
            foreach(Point point1 in points)
            {
                if (!point1.IsRedeemed)
                    return BadRequest("you must update points");
            }



            PointDto dto = await _ItemRepository.BuyItem(id, userid, quantity);
            Point point = _mapper.Map<Point>(dto);
            await _PointRepository.CreateAsync(point);
            return Ok();
        }
    }
}