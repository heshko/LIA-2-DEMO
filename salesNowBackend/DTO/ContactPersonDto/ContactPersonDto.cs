using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace salesNowBackend.DTO.ContactPersonDto
{
    public class ContactPersonDTO
    {

        //public string SystemIDPerson { get; set; }
        public string ContactId { get; set; }

        public string Name { get; set; }
     
        public string FirstName { get; set; }
 
        public string LastName { get; set; }
 
        public string Status { get; set; }
  
        public string JobTitle { get; set; }
  
        public string Email { get; set; }
       
        public string DirectTelephone { get; set; }
       
        public string Mandate { get; set; }

        public string PrimaryContact { get; set; }

        public string Comment { get; set; }

        public string Log { get; set; }
      
        public string CompanyId { get; set; }
    }
}
