using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using salesNowBackend.Contracts;
using salesNowBackend.DTO.BusinessOpportunityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace salesNowBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BusinessOpportunityController : ControllerBase
    {
        private readonly IBusinessOpportunityFirestore _businessOpportunityFirestore;
        private readonly IMapper _mapper;

        public BusinessOpportunityController(IBusinessOpportunityFirestore businessOpportunityFirestore, IMapper mapper)
        {
            _businessOpportunityFirestore = businessOpportunityFirestore;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllBusinessOpportunity()
        {
            var businessOpportunityDto = await _businessOpportunityFirestore.GetAllBusinessOpportunity("BusinessOpportunities");

            if (businessOpportunityDto is null)
            {
                return StatusCode(500, "Internal Server Error");
            }
          
            return Ok(businessOpportunityDto);
        }

        [HttpGet("{businessOpportunityId}")]
        public async Task<IActionResult> GetPersonById(string businessOpportunityId)
        {
            var businessOpportunityDTO = await _businessOpportunityFirestore.GetBusinessOpportunityById("BusinessOpportunities", businessOpportunityId);

            if (businessOpportunityDTO is null)
            {
                return NotFound($"Person with Id {businessOpportunityDTO} does not exist in Database");
            }

            return Ok(businessOpportunityDTO);
        }

    }
}
