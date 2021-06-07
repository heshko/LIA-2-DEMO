using AutoMapper;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using salesNowBackend.ActionFilter;
using salesNowBackend.Contracts;
using salesNowBackend.DTO.ActivityDto;
using salesNowBackend.DTO.CompanyActivityDto;
using salesNowBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace salesNowBackend.Controllers
{
    [Route("api/company/{companyId}/activities")]
    [ApiController]
    [Authorize]
    public class CompanyActivitesController : ControllerBase
    {
        private readonly ICompanyFirestore _companyFirestore;
        private readonly IMapper _mapper;
        private readonly IActivityFirestore _activityFirestore;

        public CompanyActivitesController(ICompanyFirestore companyFirestore, IMapper mapper, IActivityFirestore activityFirestore)
        {
            _companyFirestore = companyFirestore;
            _mapper = mapper;
            _activityFirestore = activityFirestore;
        }

        [HttpGet]
      
        public async Task<IActionResult> GetCompanyActivites(string companyId)
        {
            var companyDto = await _activityFirestore.GetCompanyActivites("Companies", companyId);
            if (companyDto is null)
            {
                return NotFound($"Company with Id {companyId} not existes in Database");
            }
            if (companyDto.Activities.Count is 0)
            {
                return Ok($"Company with Id {companyId} does not have any activites");
            }
            //var companyDto = _mapper.Map<CompanyActivityDTO>(company);

            return Ok(companyDto);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationCompanyExistsAttribute))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateActivityInCompany(string companyId, [FromBody] ActivityForCreateDTO activityForCreateDTO)
        {           
            var id = await _activityFirestore.CreateActivity("Companies", companyId, activityForCreateDTO);

            if (id is null)
            {
                return StatusCode(500, "Something went Wrong In Database");
            }

            return StatusCode(201, new { Id = id });
        }

        [HttpPut("{activityId}")]
        [ServiceFilter(typeof(ValidationCompanyExistsAttribute))]
        [ServiceFilter(typeof(ValidationActivityExistsAttribute))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateActivityInCompany(string companyId, string activityId, [FromBody] ActivityForUpdateDTO activityForUpdateDTO)
        {

            var id = await _activityFirestore.UpdateActivity("Companies", companyId, activityId , activityForUpdateDTO);

            if (id is null)
            {
                return StatusCode(500, "Something went Wrong In Database");
            }

            return Ok($"The activity with Id {activityId} has been Updated");

        }

        [HttpDelete("{activityId}")]
        [ServiceFilter(typeof(ValidationCompanyExistsAttribute))]
        [ServiceFilter(typeof(ValidationActivityExistsAttribute))]

        public async Task<IActionResult> DeleteActivityInCompany(string companyId, string activityId)
        {
           
           
            var id = await _activityFirestore.DeleteActivity("Companies", companyId, activityId);

            if (id is null)
            {
                return StatusCode(500, "Something went Wrong In Database");
            }

            return Ok($"The activity with Id {activityId} has been Deleted");

        }

    }
}
