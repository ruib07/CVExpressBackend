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
    public class HabilitationsController : ControllerBase
    {
        private readonly IHabilitationsService _habilitationsService;

        public HabilitationsController(IHabilitationsService habilitationsService)
        {
            _habilitationsService = habilitationsService;
        }

        [Authorize(Policy = "Admin")]
        // GET habilitations
        [HttpGet]
        [ProducesResponseType(typeof(List<HabilitationsEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<HabilitationsEfo>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<HabilitationsEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<HabilitationsEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<HabilitationsEfo>>> GetAllHabilitations()
        {
            List<HabilitationsEfo> habilitations = await _habilitationsService.GetAllHabilitations();

            if (habilitations == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, habilitations);
        }

        [Authorize(Policy = "AdminAndUser")]
        // GET habilitations/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(HabilitationsEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HabilitationsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(HabilitationsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(HabilitationsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetHabilitationById(int id)
        {
            HabilitationsEfo habilitation = await _habilitationsService.GetHabilitationById(id);

            if (habilitation == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, habilitation);
        }

        // POST habilitations
        [HttpPost]
        [ProducesResponseType(typeof(HabilitationsEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(HabilitationsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(HabilitationsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(HabilitationsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<HabilitationsEfo>> SendHabilitation([FromBody, Required] HabilitationsEfo habilitation)
        {
            if (ModelState.IsValid)
            {
                HabilitationsEfo habilitationPost = await _habilitationsService.SendHabilitation(habilitation);

                if (habilitationPost == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return StatusCode(StatusCodes.Status201Created, habilitationPost);
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [Authorize(Policy = "AdminAndUser")]
        // PUT habilitations/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(HabilitationsEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(HabilitationsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(HabilitationsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(HabilitationsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateHabilitation(int id, [FromBody] HabilitationsEfo updateHabilitation)
        {
            try
            {
                HabilitationsEfo newHabilitation = await _habilitationsService.UpdateHabilitation(id, updateHabilitation);

                if (newHabilitation == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return Ok(newHabilitation);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize(Policy = "Admin")]
        // DELETE habilitations/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteHabilitation(int id)
        {
            try
            {
                await _habilitationsService.DeleteHabilitation(id);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
