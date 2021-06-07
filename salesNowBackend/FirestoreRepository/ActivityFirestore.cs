using salesNowBackend.FirestoreRepository;
using salesNowBackend.Contracts;
using salesNowBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using salesNowBackend.DTO.CompanyActivityDto;
using AutoMapper;
using salesNowBackend.DTO.ActivityDto;

namespace salesNowBackend.FirestoreRepository
{
    public class ActivityFirestore : FirestoreBase<Activity>, IActivityFirestore
    {
        private readonly IFirestorRepositoryManager _firestor;
      
        private readonly IMapper _mapper;

        public ActivityFirestore(IFirestorRepositoryManager firestor, IMapper mapper) : base(firestor)
        {
            _firestor = firestor;
            _mapper = mapper;
           
        }

        public async Task<CompanyActivityDTO> GetCompanyActivites(string collectionName, string id)
        {
            CompanyActivityDTO companyActivityDTO = new CompanyActivityDTO();

            var findCompany = await _firestor.Db.Collection(collectionName).Document(id).GetSnapshotAsync();

            if (findCompany.Exists)
            {
                var companyEntity = findCompany.ConvertTo<Company>();

                companyActivityDTO = _mapper.Map<CompanyActivityDTO>(companyEntity);

                companyActivityDTO.CompanyId = findCompany.Id;
                                
                var activites = await _firestor.Db.Collection(collectionName).Document(id).Collection("Activities").WhereEqualTo("CompanyId", id).GetSnapshotAsync();
                companyActivityDTO.Activities = new List<ActivityDTO>();
                foreach (var document in activites.Documents)
                {
                    var activityEntity = document.ConvertTo<Activity>();

                    var activityDTO = _mapper.Map<ActivityDTO>(activityEntity);

                    activityDTO.ActivityId = document.Id;
                   
                    companyActivityDTO.Activities.Add(activityDTO);
                }
                return companyActivityDTO;
            }
            return null;
        }

        public async Task<List<ActivityDTO>> GetAllActivites(string collectionName)
        {
            var activites = await _firestor.Db.Collection(collectionName).GetSnapshotAsync();

            List<ActivityDTO> activitiesDTO = new List<ActivityDTO>();

            if (activites != null)
            {
                foreach (var document in activites.Documents)
                {
                    var model = document.ConvertTo<Activity>();

                    var activityDTO = _mapper.Map<ActivityDTO>(model);

                    activityDTO.ActivityId = document.Id;

                    activitiesDTO.Add(activityDTO);
                }
                return activitiesDTO;
            }

            if (activitiesDTO.Count > 0)
            {
                return activitiesDTO;
            }

            return null;
        }

        public async Task<ActivityDTO> GetActivityById(string collectionName, string id)
        {
            ActivityDTO activityDTO = new ActivityDTO();

            var findAvtivity = await _firestor.Db.Collection(collectionName).Document(id).GetSnapshotAsync();

            if (findAvtivity.Exists)
            {
                var activityEntity = findAvtivity.ConvertTo<Activity>();

                activityDTO = _mapper.Map<ActivityDTO>(activityEntity);

                activityDTO.ActivityId = findAvtivity.Id;

                return activityDTO;
            }
            return null;
        }

        public async Task<string> CreateActivity(string collectionName, string companyId, ActivityForCreateDTO activityForCreateDTO)
        {
            var activityEntity = _mapper.Map<Activity>(activityForCreateDTO);

            var id = await Create(collectionName, companyId, "Activities", activityEntity);
            if (id != null)
            {
                return id;
            }
            return null;
        }

        public async Task<string> UpdateActivity(string collectionName, string companyId, string activityId, ActivityForUpdateDTO activityForUpdateDTO)
        {

            var activityEntity = _mapper.Map<Activity>(activityForUpdateDTO);
            var id = await Update(collectionName, companyId, "Activities", activityId, activityEntity);
            if (id != null)
            {
                return id;
            }
            return null;
        }

        public async Task<string> DeleteActivity(string collectionName, string companyId, string activityId)
        {

            var id = await Delete(collectionName, companyId, "Activities", activityId);
            if (id != null)
            {
                return id;
            }
            return null;
        }
    }
}
