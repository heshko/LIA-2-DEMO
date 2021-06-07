using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using salesNowBackend.ActionFilter;
using salesNowBackend.Contracts;
using salesNowBackend.DTO.BusinessOpportunityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace salesNowBackend.Controllers
{
    [Route("api/company/{companyId}/BusinessOpportunities")]
    [ApiController]
    [Authorize]

    public class CompanyBusinessOpportunityController : ControllerBase
    {
      
        private readonly IMapper _mapper;
        private readonly IBusinessOpportunityFirestore _businessOpportunityFirestore;

        public CompanyBusinessOpportunityController(IMapper mapper, IBusinessOpportunityFirestore businessOpportunityFirestore)
        {
            
            _mapper = mapper;
            _businessOpportunityFirestore = businessOpportunityFirestore;
        }

        [HttpGet]

        public async Task<IActionResult> GetCompanyBusinessOpportunities(string companyId)
        {
            var companyDto = await _businessOpportunityFirestore.GetCompanyBusinessOpportunities("Companies", companyId);

            if (companyDto is null)
            {
                return NotFound($"Company with Id {companyId} not existes in Database");
            }
            if (companyDto.BusinessOpportunities.Count is 0)
            {
                return Ok($"Company with Id {companyId} does not have any BusinessOpportunities");
            }          

            return Ok(companyDto);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationCompanyExistsAttribute))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateBusinessOpportunityInCompany(string companyId, [FromBody] BusinessOpportunityForCreateDTO businessOpportunityForCreateDTO)
        {
            var id = await _businessOpportunityFirestore.CreateBusinessOpportunity("Companies", companyId, businessOpportunityForCreateDTO);

            if (id is null)
            {
                return StatusCode(500, "Something went Wrong In Database");
            }

            return StatusCode(201, new { Id = id });
        }

        [HttpPut("{businessOpportunityId}")]
        [ServiceFilter(typeof(ValidationCompanyExistsAttribute))]
        [ServiceFilter(typeof(ValidationBusinessOpportunityExistsAttribute))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateBusinessOpportunityInCompany(string companyId, string businessOpportunityId, [FromBody] BusinessOpportunityForUpdateDTO businessOpportunityForUpdateDTO)
        {

            var id = await _businessOpportunityFirestore.UpdateBusinessOpportunity("Companies", companyId, businessOpportunityId, businessOpportunityForUpdateDTO);

            if (id is null)
            {
                return StatusCode(500, "Something went Wrong In Database");
            }

            return Ok($"The businessOpportunity with Id {businessOpportunityId} has been Updated");

        }

        [HttpDelete("{businessOpportunityId}")]
        [ServiceFilter(typeof(ValidationCompanyExistsAttribute))]
        [ServiceFilter(typeof(ValidationBusinessOpportunityExistsAttribute))]
        public async Task<IActionResult> DeleteBusinessOpportunityInCompany(string companyId, string businessOpportunityId)
        {


            var id = await _businessOpportunityFirestore.DeleteBusinessOpportunity("Companies", companyId, businessOpportunityId);

            if (id is null)
            {
                return StatusCode(500, "Something went Wrong In Database");
            }

            return Ok($"The businessOpportunity with Id {businessOpportunityId} has been Deleted");

        }

    }
}
