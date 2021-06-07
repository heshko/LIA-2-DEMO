using salesNowBackend.FirestoreRepository;
using salesNowBackend.Contracts;
using salesNowBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using salesNowBackend.DTO.ContactPersonDto;
using AutoMapper;
using salesNowBackend.DTO.CompanyContactPersonDto;

namespace salesNowBackend.FirestoreRepository
{
    public class ContactPersonFirestore : FirestoreBase<ContactPerson>, IContactPersonFirestore
    {
        private readonly IFirestorRepositoryManager _firestor;
             
        private readonly IMapper _mapper;
        public ContactPersonFirestore(IFirestorRepositoryManager firestor, IMapper maper) : base(firestor)
        {
            _firestor = firestor;
            _mapper = maper;
        }

        public async Task<List<ContactPersonDTO>> GetAllContactPersons(string collectionName)
        {
            var contactPersons = await _firestor.Db.Collection(collectionName).GetSnapshotAsync();

            List<ContactPersonDTO> contactPersonsDTO = new List<ContactPersonDTO>();
                     
            foreach (var document in contactPersons.Documents)
            {
                var model = document.ConvertTo<ContactPerson>();

                var contactPersonDTO = _mapper.Map<ContactPersonDTO>(model);

                contactPersonDTO.ContactId = document.Id;

                contactPersonsDTO.Add(contactPersonDTO);
            }                          

            if (contactPersonsDTO.Count != 0)
            {
                return contactPersonsDTO;
            }

            return null;
        }

        public async Task<ContactPersonDTO> GetContactPersonById(string collectionName, string id)
        {
            ContactPersonDTO contactPersonDTO = new ContactPersonDTO();

            var findContactPerson = await _firestor.Db.Collection(collectionName).Document(id).GetSnapshotAsync();

            if (findContactPerson.Exists)
            {
                var contactPersonEntity = findContactPerson.ConvertTo<ContactPerson>();

                contactPersonDTO = _mapper.Map<ContactPersonDTO>(contactPersonEntity);
                              
                contactPersonDTO.ContactId = findContactPerson.Id;
                               
                return contactPersonDTO;
            }
            return null;
        }

        public async Task<List<ContactPersonDTO>> GetContactPersonsByName(string collectionName, string personName)
        {
            List<ContactPersonDTO>  contactPersonsDTO = new List<ContactPersonDTO>();

            var persons = await _firestor.Db.Collection(collectionName).GetSnapshotAsync();

            foreach (var person in persons.Documents)
            {
                var personEntity = person.ConvertTo<ContactPerson>();

                var contactPersonDTO = _mapper.Map<ContactPersonDTO>(personEntity);

                contactPersonDTO.ContactId = person.Id;

                if (contactPersonDTO.FirstName.ToUpper().Contains(personName.ToUpper()) || contactPersonDTO.LastName.ToUpper().Contains(personName.ToUpper()))
                {
                    contactPersonsDTO.Add(contactPersonDTO);
                }

            }
            if (contactPersonsDTO.Count != 0)
            {
                return contactPersonsDTO;
            }            
            
            return null;
        }

        public async Task<CompanyContactPersonDTO> GetCompanyContactPerson(string collectionName, string id)
        {
            CompanyContactPersonDTO companyDTO = new CompanyContactPersonDTO();

            var findCompany = await _firestor.Db.Collection(collectionName).Document(id).GetSnapshotAsync();

            if (findCompany.Exists)
            {
                var companyEntity = findCompany.ConvertTo<Company>();

                companyDTO = _mapper.Map<CompanyContactPersonDTO>(companyEntity);

                companyDTO.CompanyId = findCompany.Id;

                var ContactPersons = await _firestor.Db.Collection(collectionName).Document(id).Collection("ContactPersons").WhereEqualTo("CompanyId", id).GetSnapshotAsync();

                companyDTO.ContactPersons = new List<ContactPersonDTO>();

                foreach (var document in ContactPersons.Documents)
                {
                    var contactPerson = document.ConvertTo<ContactPerson>();

                    var contactPersonDTO = _mapper.Map<ContactPersonDTO>(contactPerson);

                    contactPersonDTO.ContactId = document.Id;

                    companyDTO.ContactPersons.Add(contactPersonDTO);
                }
                return companyDTO;
            }
            return null;
        }

        public async Task<string> CreateContactPerson(string collectionName, string companyId, ContactPersonForCreateDTO contactPersonForCreateDTO)
        {
            var contactPersonEntity = _mapper.Map<ContactPerson>(contactPersonForCreateDTO);
            var id = await Create(collectionName, companyId, "ContactPersons", contactPersonEntity);
            if (id != null)
            {
                return id;
            }
            return null;
        }

        public async Task<string> UpdateContactPerson(string collectionName, string companyId, string contactPersonId, ContactPersonForUpdateDTO contactPersonForUpdateDTO)
        {
            var contactPersonEntity = _mapper.Map<ContactPerson>(contactPersonForUpdateDTO);
            var id = await Update(collectionName, companyId, "ContactPersons", contactPersonId, contactPersonEntity);
            if (id != null)
            {
                return id;
            }
            return null;
        }

        public async Task<string> DeleteContactPerson(string collectionName, string companyId, string contactPersonId)
        {
            var id = await Delete(collectionName, companyId, "ContactPersons", contactPersonId);
            if (id != null)
            {
                return id;
            }
            return null;
        }
    }
}
