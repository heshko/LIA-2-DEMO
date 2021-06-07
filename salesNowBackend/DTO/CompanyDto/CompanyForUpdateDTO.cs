using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace salesNowBackend.DTO.CompanyDTO
{
    public class CompanyForUpdateDTO
    {
        [Required(ErrorMessage = "This Field Is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This Field Is Required")]
        public string OrganizationNumber { get; set; }

        public string PhoneNumber { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

        public string Industry { get; set; }
 
        public string Address { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string Comment { get; set; }

        public string ToolsAndTechnique { get; set; }

        public string Comments { get; set; }
    }
}
