using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reposetories;
using Servicess;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        IOrderServicess servicess;
        IMapper _mapper;
        public OrdersController(IOrderServicess servicess, IMapper mapper)
        {
            this.servicess = servicess;
            _mapper = mapper;
        }



        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> Get(int id)
        {
            Order order = await servicess.Get(id);
            if (order != null)
                return Ok(_mapper.Map<Order, OrderDTO>(order));
            return NotFound();



        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> Post([FromBody] OrderDTOPost order)
        {
            Order newOrder = await servicess.Post(_mapper.Map<OrderDTOPost, Order>(order));
            if (newOrder != null)
                return CreatedAtAction(nameof(Get), new { id = newOrder.Id }, _mapper.Map<Order, OrderDTO>(newOrder));
            return BadRequest();
        }


    }
}
