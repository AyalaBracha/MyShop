using Servicess;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using AutoMapper;
using DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserServicess servicess;
        IMapper _mapper;
        private readonly ILogger<UserServicess> logger;
        public UsersController(IUserServicess servicess, IMapper mapper, ILogger<UserServicess> logger)
        {
            this.servicess = servicess;
            _mapper = mapper;
            this.logger = logger;
        }





        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            User user = await servicess.Get(id);
            if (user != null)
                return Ok(_mapper.Map<User, GetByIdUserDTO>(user));
            return NotFound();



        }

        // POST api/<UsersController>
        [HttpPost]

        public async Task<ActionResult<User>> Post([FromBody] UserDTO user)
        {
            User newUser = await servicess.Post(_mapper.Map<UserDTO, User>(user));
            if (newUser != null)
                return CreatedAtAction(nameof(Get), new { id = newUser.Id }, _mapper.Map<User, GetByIdUserDTO>(newUser));
            return BadRequest();

        }
        [HttpPost("checkPassword")]

        public async Task<ActionResult<int>> CheckPassword([FromBody] string password)
        {
            int levelPassword = servicess.CheckPassword(password);

            return Ok(levelPassword);



        }

        [HttpPost("login")]

        public async Task<ActionResult<User>> Login([FromQuery] string email, [FromQuery] string password)
        {
            User user = await servicess.Login(email, password);
            if (user != null) { logger.LogInformation($"{user.Id}, {user.Email}, {user.FirstName}, {user.LastName} login to app!!");
                return Ok(_mapper.Map<User, GetByIdUserDTO>(user));
            }
               

            return NoContent();

        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(int id, [FromBody] UserDTO userToUpdate)
        {

            User user = await servicess.Put(id, _mapper.Map<UserDTO, User>(userToUpdate));
            if (user != null)
                return Ok(_mapper.Map<User, GetByIdUserDTO>(user));
            return BadRequest();


        }




    }
}
