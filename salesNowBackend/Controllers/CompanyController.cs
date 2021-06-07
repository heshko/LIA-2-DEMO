using salesNowBackend.Contracts;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using salesNowBackend.Models;
using salesNowBackend.DTO.CompanyDTO;
using Microsoft.AspNetCore.Authorization;
using salesNowBackend.ActionFilter;

namespace salesNowBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyController : ControllerBase
    {

      
        private readonly ICompanyFirestore _companyFirestore;
        private readonly IMapper _mapper;
       
        public CompanyController(IMapper mapper, ICompanyFirestore companyFireStore)
        {

            _mapper = mapper;
            _companyFirestore = companyFireStore;
        }

        [HttpGet]
       
        public async Task<IActionResult> GetAllCompanies()
        {
            var companiesDto = await  _companyFirestore.GetAllCompanies("Companies"); /*_companyFirestore.GetAllCompanies("Companies");*/
           
            if (companiesDto is null)
            {
                return StatusCode(500, "Internal Server Error");
            }
            
            return Ok(companiesDto);
        }


        [HttpGet("{companyId}")]
        public async Task<IActionResult> GetCompanyById(string companyId)
        {
            var companyDto = await _companyFirestore.GetCompaniesById("Companies", companyId);

            if (companyDto is null)
            {
                return NotFound($"Company with Id {companyId} does not exist in Database");
            }          

            return Ok(companyDto);
        }

        [HttpGet("findcompany/{companyName}")]
        public async Task<IActionResult> GetCompanyByName(string companyName)
        {
            var companiesDto = await _companyFirestore.GetCompaniesByName("Companies", companyName);

            if (companiesDto is null)
            {
                return NotFound($"Company with Name {companyName} does not exist in Database");
            }            

            return Ok(companiesDto);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreateDTO companyForCreateDTO)
        {

            var id =  await _companyFirestore.CreateCompany("Companies", companyForCreateDTO);

            if (id is null)
            {
                return StatusCode(500, "Something went Wrong In Database");
            }

            return StatusCode(201,new {Id = id });
        }

        [HttpDelete("{companyId}")]
        [ServiceFilter(typeof(ValidationCompanyExistsAttribute))]
        public async Task<IActionResult> DeleteCompany(string companyId)
        {

            var id = await _companyFirestore.DeleteCompany("Companies", companyId);

            if (id == null)
            {
                return NotFound("You Can not Delete a company Wich not existes in Database");
               
            }
            return Ok($"The Company with Id {companyId} has been deleted with all subcollections");
        }

        [HttpPut("{companyId}")]
        [ServiceFilter(typeof(ValidationCompanyExistsAttribute))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateCompany(string companyId,[FromBody] CompanyForUpdateDTO updateCompanyDto )
        {

            var id = await _companyFirestore.UpdateCompany("Companies", companyId, updateCompanyDto);

            if (id == null)
            {
                return NotFound($"You Can not Update a company Wich does not exist in Database");
            }

              return Ok($"The Company with Id {companyId} has been Updated");
        }

       
    }
}
