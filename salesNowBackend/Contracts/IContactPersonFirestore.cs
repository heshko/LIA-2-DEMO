using salesNowBackend.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using salesNowBackend.DTO.CompanyDTO;
using salesNowBackend.DTO.ContactPersonDto;
using salesNowBackend.DTO.CompanyContactPersonDto;

namespace salesNowBackend.Contracts
{
   public interface IContactPersonFirestore
    {
        Task<List<ContactPersonDTO>> GetAllContactPersons(string collectionName);
        Task<ContactPersonDTO> GetContactPersonById(string collectionName,string id);
        Task<List<ContactPersonDTO>> GetContactPersonsByName(string collectionName, string CompanyName);
        Task<CompanyContactPersonDTO> GetCompanyContactPerson(string collectionName, string id);

        Task<string> CreateContactPerson(string collectionName, string companyId, ContactPersonForCreateDTO contactPersonForCreateDTO);
        Task<string> UpdateContactPerson(string collectionName, string companyId, string contactPersonId, ContactPersonForUpdateDTO contactPersonForUpdateDTO);
        Task<string> DeleteContactPerson(string collectionName, string companyId, string contactPersonId);
    }
}
