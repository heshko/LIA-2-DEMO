using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace salesNowBackend.DTO.ContactPersonDto
{
    public class ContactPersonForCreateDTO
    {
        [Required(ErrorMessage = "This Field Is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This Field Is Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This Field Is Required")]
        public string LastName { get; set; }

       // [Required(ErrorMessage = "This Field Is Required")]
        public string Status { get; set; }

      //  [Required(ErrorMessage = "This Field Is Required")]

        public string JobTitle { get; set; }

        //  [Required(ErrorMessage = "This Field Is Required")]
        public string Email { get; set; }

        //   [Required(ErrorMessage = "This Field Is Required")]
        public string DirectTelephone { get; set; }

        //    [Required(ErrorMessage = "This Field Is Required")]
        public string Mandate { get; set; }

        //   [Required(ErrorMessage = "This Field Is Required")]
        public string PrimaryContact { get; set; }

        //   [Required(ErrorMessage = "This Field Is Required")]
        public string Comment { get; set; }

        //  [Required(ErrorMessage = "This Field Is Required")]
        public string Log { get; set; }

        [Required(ErrorMessage = "This Field Is Required")]
        public string CompanyId { get; set; }
    }
}
