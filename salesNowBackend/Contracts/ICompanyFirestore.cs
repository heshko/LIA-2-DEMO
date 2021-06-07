using salesNowBackend.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using salesNowBackend.DTO.CompanyDTO;
using salesNowBackend.DTO.CompanyContactPersonDto;

namespace salesNowBackend.Contracts
{
   public interface ICompanyFirestore 
    {
        Task<List<CompanyDTO>> GetAllCompanies(string collectionName);
        Task<CompanyDTO> GetCompaniesById(string collectionName, string id);
        Task<List<CompanyDTO>> GetCompaniesByName(string collectionName,string CompanyName);      
        Task<string> CreateCompany(string collectionName, CompanyForCreateDTO comapnyDTO);
        Task<string> DeleteCompany(string collectionName, string id);
        Task<string> UpdateCompany(string collectionName, string id, CompanyForUpdateDTO comapnyEntity);          
        //Task<CompanyContactPersonDTO> GetCompanyContactPerson(string collectionName, string id);

        //Task<List<Company>> GetCompaniesWithAll(string collectionName);


    }
}
