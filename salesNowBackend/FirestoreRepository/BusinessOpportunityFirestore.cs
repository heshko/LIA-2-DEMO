using AutoMapper;
using salesNowBackend.Contracts;
using salesNowBackend.DTO.BusinessOpportunityDto;
using salesNowBackend.DTO.CompanyBusinessOpportunityDTO;
using salesNowBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace salesNowBackend.FirestoreRepository
{
    public class BusinessOpportunityFirestore : FirestoreBase<BusinessOpportunity>, IBusinessOpportunityFirestore
    {
        private readonly IFirestorRepositoryManager _firestor;
        private readonly IMapper _mapper;
        public BusinessOpportunityFirestore(IFirestorRepositoryManager firestor, IMapper mapper) : base(firestor)
        {
            _firestor = firestor;
            _mapper = mapper;
        }

        public async Task<List<BusinessOpportunityDTO>> GetAllBusinessOpportunity(string collectionName)
        {
            var businessOpportunities = await _firestor.Db.Collection(collectionName).GetSnapshotAsync();

            List<BusinessOpportunityDTO> businessOpportunitiesDTO = new List<BusinessOpportunityDTO>();

            if (businessOpportunities != null)
            {
                foreach (var document in businessOpportunities.Documents)
                {
                    var entity = document.ConvertTo<Models.BusinessOpportunity>();

                    var businessOpportunityDTO = _mapper.Map<BusinessOpportunityDTO>(entity);

                    businessOpportunityDTO.BussinessId = document.Id;

                    businessOpportunitiesDTO.Add(businessOpportunityDTO);
                }
                return businessOpportunitiesDTO;
            }
            if (businessOpportunitiesDTO.Count > 0)
            {
                return businessOpportunitiesDTO;
            }
            return null;
        }
        public async Task<BusinessOpportunityDTO> GetBusinessOpportunityById(string collectionName, string id)
        {
            BusinessOpportunityDTO businessOpportunityDTO = new BusinessOpportunityDTO();

            var findbusinessOpportunity = await _firestor.Db.Collection(collectionName).Document(id).GetSnapshotAsync();

            if (findbusinessOpportunity.Exists)
            {
                var businessOpportunityEntity = findbusinessOpportunity.ConvertTo<BusinessOpportunity>();

                businessOpportunityDTO = _mapper.Map<BusinessOpportunityDTO>(businessOpportunityEntity);

                businessOpportunityDTO.BussinessId = findbusinessOpportunity.Id;

                return businessOpportunityDTO;
            }
            return null;
        }

        public async Task<string> CreateBusinessOpportunity(string collectionName, string companyId, BusinessOpportunityForCreateDTO businessOpportunityForCreateDTO)
        {
            var businessOpportunityEntity = _mapper.Map<BusinessOpportunity>(businessOpportunityForCreateDTO);
            

            var id = await Create(collectionName, companyId, "BusinessOpportunities", businessOpportunityEntity);
            if (id != null)
            {
                return id;
            }
            return null;
        }

        public async Task<string> DeleteBusinessOpportunity(string collectionName, string companyId, string businessOpportunityId)
        {
            var id = await Delete(collectionName, companyId, "BusinessOpportunities", businessOpportunityId);
            if (id != null)
            {
                return id;
            }
            return null;
        }

       

        public async Task<CompanyBusinessOpportunityDTO> GetCompanyBusinessOpportunities(string collectionName, string id)
        {
            CompanyBusinessOpportunityDTO companyBusinessOpportunityDTO = new CompanyBusinessOpportunityDTO();

            var findCompany = await _firestor.Db.Collection(collectionName).Document(id).GetSnapshotAsync();

            if (findCompany.Exists)
            {
                var companyEntity = findCompany.ConvertTo<Company>();

                companyBusinessOpportunityDTO = _mapper.Map<CompanyBusinessOpportunityDTO>(companyEntity);

                companyBusinessOpportunityDTO.CompanyId = findCompany.Id;

                var businessOpportunity = await _firestor.Db.Collection(collectionName).Document(id).Collection("BusinessOpportunities").WhereEqualTo("CompanyId", id).GetSnapshotAsync();

                companyBusinessOpportunityDTO.BusinessOpportunities = new List<BusinessOpportunityDTO>();

                foreach (var document in businessOpportunity.Documents)
                {
                    var businessOpportunityEntity = document.ConvertTo<BusinessOpportunity>();

                    var businessOpportunityDTO = _mapper.Map<BusinessOpportunityDTO>(businessOpportunityEntity);

                    businessOpportunityDTO.BussinessId = document.Id;

                    companyBusinessOpportunityDTO.BusinessOpportunities.Add(businessOpportunityDTO);
                }
                return companyBusinessOpportunityDTO;
            }
            return null;
        }

        public async Task<string> UpdateBusinessOpportunity(string collectionName, string companyId, string businessOpportunityId, BusinessOpportunityForUpdateDTO businessOpportunityForUpdateDTO)
        {
            var businessOpportunityEntity = _mapper.Map<BusinessOpportunity>(businessOpportunityForUpdateDTO);
            var id = await Update(collectionName, companyId, "BusinessOpportunities", businessOpportunityId, businessOpportunityEntity);
            if (id != null)
            {
                return id;
            }
            return null;
        }
    }
}
