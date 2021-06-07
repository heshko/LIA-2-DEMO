using salesNowBackend.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using salesNowBackend.DTO.CompanyDTO;
using salesNowBackend.DTO.CompanyActivityDto;
using salesNowBackend.DTO.ActivityDto;

namespace salesNowBackend.Contracts
{
   public interface IActivityFirestore
    {
        Task<List<ActivityDTO>> GetAllActivites(string collectionName);
        Task<CompanyActivityDTO> GetCompanyActivites(string collectionName, string id);
        Task<ActivityDTO> GetActivityById(string collectionName,string id);
        Task<string> CreateActivity(string collectionName, string companyId, ActivityForCreateDTO activityForCreateDTO);
        Task<string> UpdateActivity(string collectionName, string companyId, string activityId,ActivityForUpdateDTO activityForUpdateDTO);
        Task<string> DeleteActivity(string collectionName, string companyId, string activityId);
    }
}


