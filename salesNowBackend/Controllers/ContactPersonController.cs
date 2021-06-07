using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using salesNowBackend.Contracts;
using salesNowBackend.DTO.ContactPersonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace salesNowBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactPersonController : ControllerBase
    {

        private readonly IContactPersonFirestore _contactPersonFirestore;
        private readonly IMapper _mapper;
        public ContactPersonController(IContactPersonFirestore contactPersonFirestore, IMapper mapper)
        {
            _contactPersonFirestore = contactPersonFirestore;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContactPersons()
        {
            var contactPersonsDTO = await _contactPersonFirestore.GetAllContactPersons("ContactPersons");

            if (contactPersonsDTO is null)
            {
                return StatusCode(500, "Internal Server Error");
            }
            
            return Ok(contactPersonsDTO);
        }

        [HttpGet("{contactPersonId}")]
        public async Task<IActionResult> GetPersonById(string contactPersonId)
        {
            var contactPersonDTO = await _contactPersonFirestore.GetContactPersonById("ContactPersons", contactPersonId);

            if (contactPersonDTO is null)
            {
                return NotFound($"Person with Id {contactPersonId} does not exist in Database");
            }
           
            return Ok(contactPersonDTO);
        }

        [HttpGet("findPerson/{PersonName}")]
        public async Task<IActionResult> GetPersonByName(string PersonName)
        {
            var contactPersonDTO = await _contactPersonFirestore.GetContactPersonsByName("ContactPersons", PersonName);

            if (contactPersonDTO is null)
            {
                return NotFound($"Person with Name {PersonName} does not exist in Database");
            }
          
            return Ok(contactPersonDTO);
        }
    }
}
