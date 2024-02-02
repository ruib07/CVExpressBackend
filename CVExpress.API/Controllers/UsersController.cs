using CVExpress.Entities.Efos;
using CVExpress.Services.Interfaces;
using CVExpress.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace CVExpress.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [Authorize(Policy = "AdminAndUser")]
        // GET users
        [HttpGet]
        [ProducesResponseType(typeof(List<UsersEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<UsersEfo>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<UsersEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<UsersEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<UsersEfo>>> GetAllUsers()
        {
            List<UsersEfo> users = await _usersService.GetAllUsers();

            if (users == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, users);
        }

        [Authorize(Policy = "AdminAndUser")]
        // GET users/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UsersEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UsersEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UsersEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UsersEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            UsersEfo user = await _usersService.GetUserById(id);

            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, user);
        }

        // POST users
        [HttpPost]
        [ProducesResponseType(typeof(UsersEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(UsersEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UsersEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UsersEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<UsersEfo>> SendUser([FromBody, Required] UsersEfo user)
        {
            if (ModelState.IsValid)
            {
                UsersEfo userPost = await _usersService.SendUser(user);

                if (userPost == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return StatusCode(StatusCodes.Status201Created, userPost);
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [Authorize(Policy = "AdminAndUser")]
        // PUT users/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UsersEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(UsersEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UsersEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UsersEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UsersEfo updateUser)
        {
            try
            {
                UsersEfo newUser = await _usersService.UpdateUser(id, updateUser);

                if (newUser == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return Ok(newUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize(Policy = "Admin")]
        // DELETE users/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _usersService.DeleteUser(id);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
