using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneBookApplication.Core.Entities;
using PhoneBookApplication.Core.Models.DTO;
using PhoneBookApplication.Core.Models.Pagination;
using PhoneBookApplication.Core.Services.InfrastructureServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookApplication.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IPhoneBookQueryCommand<Contact> _phoneBookQueryCommand;
        private readonly ILogger<ContactsController> _logger;
        private readonly IMapper _mapper;


        public ContactsController(IPhoneBookQueryCommand<Contact> phoneBookQueryCommand, ILogger<ContactsController> logger, IMapper mapper)
        {
            _phoneBookQueryCommand = phoneBookQueryCommand;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all Contacts.
        /// </summary>
        ///<response code="200">Returned all phonebook entries or empty array</response>
        ///
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllContactsAsync([FromQuery] RequestParams requestParams)
        {
            var contacts = await _phoneBookQueryCommand.GetAllAsync(requestParams);
            var results = _mapper.Map<IList<ContactResponseDTO>>(contacts);
            return Ok(results);
        }

        /// <summary>
        /// Get Contact by id
        /// </summary>
        ///<response code="200">Returned single phonebook entry or empty array</response>
        ///
        [HttpGet("{id:int}", Name = "GetContactAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetContactAsync(int id)
        {
            if (id != 0)
            {
                var contact = await _phoneBookQueryCommand.GetByIdAsync(id);
                if (contact == null)
                {
                    _logger.LogError($"Invalid GET attemp in {nameof(GetContactAsync)}");
                    return NotFound();
                }
                var results = _mapper.Map<ContactResponseDTO>(contact);
                return Ok(results);
            }
            return BadRequest();
        }

        /// <summary>
        /// Add a new Contact.
        /// </summary>
        ///<response code="201">Returned for entry added successfuly</response>
        ///
        //[Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddNewContactToPhonebookAsync([FromBody] ContactRequestDTO contactDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attemp in {nameof(AddNewContactToPhonebookAsync)}");
                return BadRequest(ModelState);
            }

            var contact = _mapper.Map<Contact>(contactDto);
            await _phoneBookQueryCommand.AddAsync(contact);
            
            return CreatedAtRoute("GetContactAsync", new { id = contact.Id }, contact);
        }


        /// <summary>
        /// Edit an existing contact.
        /// </summary>
        ///<response code="204">Returned when contact edited successfully</response>
        ///<response code="400">Returned when id in request route doesnt match request body</response>
        ///<response code="404">Returned when contact does not exist</response>
        ///
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize]
        public async Task<IActionResult> UpdateContactAsync(int id, [FromBody] UpdateContactDTO contactDto)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateContactAsync)}");
                return BadRequest(ModelState);
            }

            var contact = await _phoneBookQueryCommand.GetByIdAsync(id);
            if (contact == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateContactAsync)}");
                return BadRequest(ModelState);
            }
            
            _mapper.Map(contactDto, contact);
            await _phoneBookQueryCommand.UpdateAsync(contact);

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        /// <summary>
        /// Delete an existing contact.
        /// </summary>
        ///<response code="204">Returned when contact deleted successfully</response>
        ///<response code="404">Returned when contact not found</response>
        ///
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteContactAsync(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteContactAsync)}");
                return NotFound();
            }

            var contact = await _phoneBookQueryCommand.GetByIdAsync(id);
            if (contact == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteContactAsync)}");
                return NotFound("Invalid Data");
            }
            await _phoneBookQueryCommand.DeleteAsync(id);

            return NoContent();
        }

        [HttpGet("filter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FilterByNameOrPhoneNumberAsync(string searchString)
        {
            var allContacts = await _phoneBookQueryCommand.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredresult = allContacts.Where(c =>
                string.Equals(c.Name, searchString,
                StringComparison.CurrentCultureIgnoreCase) ||
                string.Equals(c.PhoneNumber, searchString,
                StringComparison.CurrentCultureIgnoreCase)).ToList();

                if (filteredresult.Count == 0)
                {
                    _logger.LogError($"Invalid Filter attempt in {nameof(FilterByNameOrPhoneNumberAsync)}");
                    return NotFound("Search not found");
                }

                return Ok(filteredresult);
            }
            return BadRequest();
        }
    }
}
