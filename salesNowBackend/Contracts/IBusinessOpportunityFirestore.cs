using salesNowBackend.DTO.BusinessOpportunityDto;
using salesNowBackend.DTO.CompanyBusinessOpportunityDTO;
using salesNowBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace salesNowBackend.Contracts
{
    public interface IBusinessOpportunityFirestore
    {
        Task<List<BusinessOpportunityDTO>> GetAllBusinessOpportunity(string collectionName);
        Task<BusinessOpportunityDTO> GetBusinessOpportunityById(string collectionName, string Id);
        Task<CompanyBusinessOpportunityDTO> GetCompanyBusinessOpportunities(string collectionName, string id);
        Task<string> CreateBusinessOpportunity(string collectionName, string companyId, BusinessOpportunityForCreateDTO businessOpportunityForCreateDTO);
        Task<string> UpdateBusinessOpportunity(string collectionName, string companyId, string businessOpportunityId, BusinessOpportunityForUpdateDTO businessOpportunityForUpdateDTO);
        Task<string> DeleteBusinessOpportunity(string collectionName, string companyId, string businessOpportunityId);

    }
}
