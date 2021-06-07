using salesNowBackend.Contracts;
using salesNowBackend.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using salesNowBackend.DTO.CompanyDTO;
using AutoMapper;
using Newtonsoft.Json;
using salesNowBackend.DTO.CompanyContactPersonDto;
using salesNowBackend.DTO.ContactPersonDto;

namespace salesNowBackend.FirestoreRepository

{
    public class CompanyFirestore : FirestoreBase<Company>, ICompanyFirestore
    {
        private readonly IFirestorRepositoryManager _firestor;
        private readonly IMapper _mapper;
        public CompanyFirestore(IFirestorRepositoryManager firestor,IMapper maper) : base(firestor)
        {
            _firestor = firestor;
            _mapper = maper;
        }


        public async Task<List<CompanyDTO>> GetAllCompanies(string collectionName)
        {
            var compnies = await _firestor.Db.Collection(collectionName).GetSnapshotAsync();

            List<CompanyDTO> Entites = new List<CompanyDTO>();
          
            if (compnies.Documents.Count != 0)
            {
                foreach (var document in compnies.Documents)
                {
                    var model = document.ConvertTo<Company>();

                    var modelDto = _mapper.Map<CompanyDTO>(model);

                    modelDto.CompanyId = document.Id;

                    Entites.Add(modelDto);
                }
                return Entites;
            }

            if (Entites.Count > 0)
            {
                return Entites;
            }

            return null;
        }

        public async Task<CompanyDTO> GetCompaniesById(string collectionName, string id)
        {
            CompanyDTO companyDto = new CompanyDTO();

            var company = await _firestor.Db.Collection(collectionName).Document(id).GetSnapshotAsync();

            if (company.Exists)
            {
                var companyEntity = company.ConvertTo<Company>();

                companyDto = _mapper.Map<CompanyDTO>(companyEntity);

                companyDto.CompanyId = company.Id;            

                return companyDto;
            }
            return null;
        }

        public async Task<List<CompanyDTO>> GetCompaniesByName(string collectionName, string CompanyName)
        {
            List<CompanyDTO> companiesDto = new List<CompanyDTO>();

            var companies = await _firestor.Db.Collection(collectionName).GetSnapshotAsync();

                foreach (var company in companies.Documents)
                {
                    var companyEntity = company.ConvertTo<Company>();

                    var companyDto = _mapper.Map<CompanyDTO>(companyEntity);

                    companyDto.CompanyId = company.Id;
                
                    if (companyDto.Name.ToUpper().Contains(CompanyName.ToUpper()))
                    {
                        companiesDto.Add(companyDto);
                    }

                }

            if (companiesDto.Count != 0)
            {
                return companiesDto;
            }

            return null;
        }
        public async Task<string> CreateCompany(string collectionName, CompanyForCreateDTO companyForCreateDTO)
        {
            var entity = _mapper.Map<Company>(companyForCreateDTO);

            var newComapny = await _firestor.Db.Collection(collectionName).AddAsync(entity);
            
            if (newComapny.Id != null)
            {
                return newComapny.Id;
            }       

            return null;
        }

        public async Task<string> DeleteCompany(string collectionName, string id)
        {
            var document = await _firestor.Db.Collection(collectionName).Document(id).GetSnapshotAsync();

            if (document.Exists)
            {             

                await DeleteSubCollection("Activities", id);
                await DeleteSubCollection("BusinessOpportunities", id);
                await DeleteSubCollection("ContactPersons", id);
                await DeleteCompanyFromOtherCollections("Activities", id);
                await DeleteCompanyFromOtherCollections("BusinessOpportunities", id);
                await DeleteCompanyFromOtherCollections("ContactPersons", id);
                await document.Reference.DeleteAsync();

                return id;
            }
            
            return null;
        }

        public async Task<string> UpdateCompany(string collectionName, string id ,CompanyForUpdateDTO companyForUpdateDTO)
        {
            var document =await _firestor.Db.Collection(collectionName).Document(id).GetSnapshotAsync();

            if (document.Exists)
            {
                var companyEntity = _mapper.Map<Company>(companyForUpdateDTO);

                var companyJson = JsonConvert.SerializeObject(companyEntity);

                var companyDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(companyJson);

                await _firestor.Db.Collection(collectionName).Document(id).UpdateAsync(companyDictionary);

                if (document.GetValue<string>("Name") != companyEntity.Name)
                {
                   var activities =  await _firestor.Db.Collection(collectionName).Document(id).Collection("Activities").GetSnapshotAsync();
                   var businessOpportunities = await _firestor.Db.Collection(collectionName).Document(id).Collection("BusinessOpportunities").GetSnapshotAsync();
                   var contactPersons = await _firestor.Db.Collection(collectionName).Document(id).Collection("ContactPersons").GetSnapshotAsync();

                   await UpdateCompanyNameInsideSubCollection(id, activities, companyEntity.Name, "Activities");
                   await UpdateCompanyNameInsideSubCollection(id, businessOpportunities, companyEntity.Name, "BusinessOpportunities");
                   await UpdateCompanyNameInsideSubCollection(id, contactPersons, companyEntity.Name, "ContactPersons");

                   await UpdateCompanyNameInsideCollection(id, companyEntity.Name, "Activities");
                   await UpdateCompanyNameInsideCollection(id, companyEntity.Name, "BusinessOpportunities");
                   await UpdateCompanyNameInsideCollection(id, companyEntity.Name, "ContactPersons");

                }

                return id;
            }
            return null;
        }


        //public async Task<List<Company>> GetCompaniesWithAll(string collectionName)
        //{
        //    List<Company> companiesEntity = new List<Company>();
        //    QuerySnapshot companies = await FindAll(collectionName);

        //    if (companies != null)
        //    {
        //        foreach (var company in companies.Documents)
        //        {

        //            var companyEntity = company.ConvertTo<Company>();
        //            companyEntity.CompanyId = company.Id;
        //            var activities = await _firestor.Db.Collection(collectionName).Document(company.Id).Collection("Activities").GetSnapshotAsync();
        //            if (activities != null)
        //            {
        //                foreach (var activity in activities.Documents)
        //                {

        //                    var activityEntity = activity.ConvertTo<Activity>();
        //                    activityEntity.ActivityId = activity.Id;
        //                    companyEntity.Activities.Add(activityEntity);
        //                }
        //            }
        //            var ContactPersons = await _firestor.Db.Collection(collectionName).Document(company.Id).Collection("ContactPersons").GetSnapshotAsync();
        //            if (ContactPersons != null)
        //            {
        //                foreach (var contactPerson in ContactPersons.Documents)
        //                {

        //                    var contactPersonEntity = contactPerson.ConvertTo<ContactPerson>();
        //                    contactPersonEntity.ContactId = contactPerson.Id;
        //                    companyEntity.ContactPersons.Add(contactPersonEntity);
        //                }
        //            }
        //            var BusinessOpportunities = await _firestor.Db.Collection(collectionName).Document(company.Id).Collection("BusinessOpportunities").GetSnapshotAsync();
        //            if (BusinessOpportunities != null)
        //            {
        //                foreach (var businessOpportunity in BusinessOpportunities.Documents)
        //                {

        //                    var businessOpportunityEntity = businessOpportunity.ConvertTo<BusinessOpportunity>();
        //                    businessOpportunityEntity.BussinessId = businessOpportunity.Id;
        //                    companyEntity.BusinessOpportunities.Add(businessOpportunityEntity);
        //                }
        //            }
        //            companiesEntity.Add(companyEntity);



        //        }

        //    }
        //    return companiesEntity;
        //}
       

        public async Task DeleteSubCollection(string subCollectionName, string id)
        {
            var documentsInSubCollection = await _firestor.Db.Collection("Companies").Document(id).Collection(subCollectionName).GetSnapshotAsync();

            if (documentsInSubCollection.Documents.Count != 0)
            {
                foreach (var documentInSubCollection in documentsInSubCollection.Documents)
                {
                    await documentInSubCollection.Reference.DeleteAsync();
                }
            }
        }
        public async Task DeleteCompanyFromOtherCollections(string collectionName, string id)
        {
            var collection = await _firestor.Db.Collection(collectionName).WhereEqualTo("CompanyId", id).GetSnapshotAsync();

            if (collection.Documents.Count != 0)
            {
                foreach (var document in collection.Documents)
                {
                    await document.Reference.DeleteAsync();
                }
            }
        }


       public async Task UpdateCompanyNameInsideSubCollection(string id, QuerySnapshot subcollection, string companyNewName, string subCollactionName )
        {
            if (subcollection.Count != 0)
            {
                foreach (var document in subcollection.Documents)
                {
                    Dictionary<string, string> updateComanyName = new Dictionary<string, string>
                    {
                        {
                            "Name" , companyNewName
                        }
                    };

                    await _firestor.Db.Collection("Companies").Document(id).Collection(subCollactionName).Document(document.Id).SetAsync(updateComanyName, SetOptions.MergeAll);
                }
            }
        }
       public async Task UpdateCompanyNameInsideCollection(string id, string companyNewName, string CollactionName)
        {
            var collection = await _firestor.Db.Collection(CollactionName).WhereEqualTo("CompanyId",id).GetSnapshotAsync();

            if (collection.Count != 0)
            {

                foreach (var document in collection.Documents)
                {
                    Dictionary<string, string> updateComanyName = new Dictionary<string, string>
                    {
                        {
                            "Name" , companyNewName
                        }
                    };

                    await _firestor.Db.Collection(CollactionName).Document(document.Id).SetAsync(updateComanyName, SetOptions.MergeAll);
                }
            }
         
        }

    }
}