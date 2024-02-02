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
    public class ExperienceController : ControllerBase
    {
        private readonly IExperienceService _experienceService;

        public ExperienceController(IExperienceService experienceService)
        {
            _experienceService = experienceService;
        }

        [Authorize(Policy = "AdminAndUser")]
        // GET experiences
        [HttpGet]
        [ProducesResponseType(typeof(List<ExperienceEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ExperienceEfo>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<ExperienceEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<ExperienceEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<ExperienceEfo>>> GetAllExperiences()
        {
            List<ExperienceEfo> experiences = await _experienceService.GetAllExperiences();

            if (experiences == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, experiences);
        }

        [Authorize(Policy = "AdminAndUser")]
        // GET experiences/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ExperienceEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExperienceEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ExperienceEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExperienceEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetExperienceById(int id)
        {
            ExperienceEfo experience = await _experienceService.GetExperienceById(id);

            if (experience == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, experience);
        }

        // POST experiences
        [HttpPost]
        [ProducesResponseType(typeof(ExperienceEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ExperienceEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ExperienceEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExperienceEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ExperienceEfo>> SendExperience([FromBody, Required] ExperienceEfo experience)
        {
            if (ModelState.IsValid)
            {
                ExperienceEfo experiencePost = await _experienceService.SendExperience(experience);

                if (experiencePost == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return StatusCode(StatusCodes.Status201Created, experiencePost);
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [Authorize(Policy = "AdminAndUser")]
        // PUT experiences/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ExperienceEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ExperienceEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ExperienceEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExperienceEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateExperience(int id, [FromBody] ExperienceEfo updateExperience)
        {
            try
            {
                ExperienceEfo newExperience = await _experienceService.UpdateExperience(id, updateExperience);

                if (newExperience == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return Ok(newExperience);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize(Policy = "Admin")]
        // DELETE experiences/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteExperience(int id)
        {
            try
            {
                await _experienceService.DeleteExperience(id);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
