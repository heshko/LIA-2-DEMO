using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using salesNowBackend.ActionFilter;
using salesNowBackend.Contracts;
using salesNowBackend.DTO.ActivityDto;
using salesNowBackend.DTO.CompanyActivityDto;
using salesNowBackend.DTO.CompanyContactPersonDto;
using salesNowBackend.DTO.CompanyDTO;
using salesNowBackend.DTO.ContactPersonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace salesNowBackend.Controllers
{
    [Route("api/company/{companyId}/contactPersons")]
    [ApiController]
    [Authorize]
    public class CompanyContactPersonController : ControllerBase
    {
        private readonly IContactPersonFirestore _contactPersonFirestore;
        private readonly IMapper _mapper;

        public CompanyContactPersonController(IContactPersonFirestore contactPersonFirestore, IMapper mapper)
        {
            _contactPersonFirestore = contactPersonFirestore;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanyContactPersons(string companyId)
        {
            var companyDto = await _contactPersonFirestore.GetCompanyContactPerson("Companies", companyId);
            if (companyDto is null)
            {
                return NotFound($"Company with Id {companyId} not existes in Database");
            }
            if (companyDto.ContactPersons.Count == 0)
            {
                return Ok($"Company with Id {companyId} does not have any Contact Person");
            }

            return Ok(companyDto);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationCompanyExistsAttribute))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateContactPersonInCompany(string companyId, [FromBody] ContactPersonForCreateDTO contactPersonForCreateDTO)
        {
            var id = await _contactPersonFirestore.CreateContactPerson("Companies", companyId, contactPersonForCreateDTO);

            if (id is null)
            {
                return StatusCode(500, "Something went Wrong In Database");
            }

            return StatusCode(201, new { Id = id });
        }

        [HttpPut("{contactPersonId}")]
        [ServiceFilter(typeof(ValidationCompanyExistsAttribute))]
        [ServiceFilter(typeof(ValidationContactPersonExistsAttribute))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateContactPersonInCompany(string companyId, string contactPersonId, [FromBody] ContactPersonForUpdateDTO contactPersonForUpdateDTO)
        {

            var id = await _contactPersonFirestore.UpdateContactPerson("Companies", companyId, contactPersonId, contactPersonForUpdateDTO);

            if (id is null)
            {
                return StatusCode(500, "Something went Wrong In Database");
            }

            return Ok($"The contactperson with Id {contactPersonId} has been Updated");

        }

        [HttpDelete("{contactPersonId}")]
        [ServiceFilter(typeof(ValidationCompanyExistsAttribute))]
        [ServiceFilter(typeof(ValidationContactPersonExistsAttribute))]

        public async Task<IActionResult> DeleteContactPersonInCompany(string companyId, string contactPersonId)
        {


            var id = await _contactPersonFirestore.DeleteContactPerson("Companies", companyId, contactPersonId);

            if (id is null)
            {
                return StatusCode(500, "Something went Wrong In Database");
            }

            return Ok($"The contactperson with Id {contactPersonId} has been Deleted");

        }
    }
}
