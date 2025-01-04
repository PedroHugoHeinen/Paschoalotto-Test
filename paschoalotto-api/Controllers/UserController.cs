using Microsoft.AspNetCore.Mvc;
using paschoalotto_api.Globals.DTOs;
using paschoalotto_api.Services.Interfaces;

namespace paschoalotto_api.Controllers
{
    [ApiController]
    [Route("PaschoalottoApi/User")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllAsync()
        {
            var response = await this.userService.GetAllAsync();

            if (response?.Any() != true)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<UserDTO>> GetByIdAsync(int id)
        {
            var response = await this.userService.GetByIdAsync(id);

            if (response == default)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost("Insert")]
        public async Task<ActionResult<UserDTO>> InsertAsync(UserDTO userDTO)
        {
            var response = await this.userService.InsertAsync(userDTO);

            if (response == default)
            {
                return BadRequest();
            }

            return Ok(response);
        }
        
        [HttpPut("Update")]
        public async Task<ActionResult<UserDTO>> UpdateAsync(UserDTO userDTO)
        {
            var response = await this.userService.UpdateAsync(userDTO);

            if (response == default)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(int id)
        {
            var response = await this.userService.DeleteAsync(id);

            if (!response)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("GetRandom")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetRandomAsync()
        {
            var response = await this.userService.GetRandomAsync();

            if (response == default)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}