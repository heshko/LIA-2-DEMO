using salesNowBackend.DTO.ActivityDto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace salesNowBackend.DTO.CompanyActivityDto
{
    public class CompanyActivityDTO
    {
        //public string SystemIDOrganisation { get; set; }
        public string CompanyId { get; set; }

        public string Name { get; set; }

        //public string Organisationsnummer { get; set; }
        public string OrganizationNumber { get; set; }

        //public string Telephone { get; set; }
        public string PhoneNumber { get; set; }
        public string Type { get; set; }

        public string Status { get; set; }

        public string Industry { get; set; }

        public string Address { get; set; }

        //public string PostCodeZIP { get; set; }
        public string ZipCode { get; set; }

        public string City { get; set; }

        public string Comment { get; set; }

        public string ToolsAndTechnique { get; set; }

        public string Comments { get; set; }
        public List<ActivityDTO> Activities { get; set; }
    }
}
