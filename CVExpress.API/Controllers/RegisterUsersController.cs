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
    public class RegisterUsersController : ControllerBase
    {
        private readonly IRegisterUsersService _registerUsersService;

        public RegisterUsersController(IRegisterUsersService registerUsersService)
        {
            _registerUsersService = registerUsersService;
        }

        [Authorize(Policy = "Admin")]
        // GET registerusers
        [HttpGet]
        [ProducesResponseType(typeof(List<RegisterUsersEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<RegisterUsersEfo>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<RegisterUsersEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<RegisterUsersEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<RegisterUsersEfo>>> GetAllRegisterUsers()
        {
            List<RegisterUsersEfo> registerUsers = await _registerUsersService.GetAllRegisterUsers();

            if (registerUsers == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, registerUsers);
        }

        [Authorize(Policy = "AdminAndUser")]
        // GET registerusers/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetRegisterUserByIdAsync(int id)
        {
            RegisterUsersEfo registerUser = await _registerUsersService.GetRegisterUserById(id);

            if (registerUser == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, registerUser);
        }

        // POST registerusers
        [HttpPost]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<RegisterUsersEfo>> SendRegisterUser([FromBody, Required] RegisterUsersEfo registerUser)
        {
            if (ModelState.IsValid)
            {
                RegisterUsersEfo registerUserPost = await _registerUsersService.SendRegisterUser(registerUser);

                if (registerUserPost == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return StatusCode(StatusCodes.Status201Created, registerUserPost);
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [Authorize(Policy = "AdminAndUser")]
        // PUT registerusers/{email}/updatepassword
        [HttpPut("{email}/updatepassword")]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdatePassword(string email, string newPassword, string confirmPassword)
        {
            try
            {
                RegisterUsersEfo password = await _registerUsersService.UpdatePassword(email, newPassword, confirmPassword);

                if (password == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return Ok(password);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar password: {ex.Message}");
            }
        }

        [Authorize(Policy = "AdminAndUser")]
        // PUT registerusers/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateRegisterUser(int id, [FromBody] RegisterUsersEfo updateRegisterUser)
        {
            try
            {
                RegisterUsersEfo newRegisterUser = await _registerUsersService.UpdateRegisterUser(id, updateRegisterUser);

                if (newRegisterUser == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return Ok(newRegisterUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize(Policy = "Admin")]
        // DELETE registerusers/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteRegisterUser(int id)
        {
            try
            {
                await _registerUsersService.DeleteRegisterUser(id);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
