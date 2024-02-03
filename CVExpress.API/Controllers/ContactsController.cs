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
    public class ContactsController : ControllerBase
    {
        private readonly IContactsService _contactsService;

        public ContactsController(IContactsService contactsService)
        {
            _contactsService = contactsService;
        }

        [Authorize(Policy = "AdminAndUser")]
        // GET contacts
        [HttpGet]
        [ProducesResponseType(typeof(List<ContactsEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ContactsEfo>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<ContactsEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<ContactsEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<ContactsEfo>>> GetAllContacts()
        {
            List<ContactsEfo> contacts = await _contactsService.GetAllContacts();

            if (contacts == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, contacts);
        }

        [Authorize(Policy = "AdminAndUser")]
        // GET contacts/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ContactsEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ContactsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ContactsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ContactsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetContactById(int id)
        {
            ContactsEfo contact = await _contactsService.GetContactById(id);

            if (contact == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, contact);
        }

        // POST contacts
        [HttpPost]
        [ProducesResponseType(typeof(ContactsEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ContactsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ContactsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ContactsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ContactsEfo>> SendContact([FromBody, Required] ContactsEfo contact)
        {
            if (ModelState.IsValid)
            {
                ContactsEfo contactPost = await _contactsService.SendContact(contact);

                if (contactPost == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return StatusCode(StatusCodes.Status201Created, contactPost);
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [Authorize(Policy = "Admin")]
        // DELETE contacts/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteContact(int id)
        {
            try
            {
                await _contactsService.DeleteContact(id);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
