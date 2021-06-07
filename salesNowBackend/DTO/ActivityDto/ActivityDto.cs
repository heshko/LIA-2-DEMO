using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace salesNowBackend.DTO.ActivityDto
{

    public class ActivityDTO
    {
        public string ActivityId { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public DateTime Date { get; set; }

        public string Action { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public string Comment { get; set; }

        public string Log { get; set; }

       // public string SystemIDOrganisation { get; set; }
        public string CompanyId { get; set; }

        // public string SystemIDPerson { get; set; }
        public string ContactId { get; set; }

    }
}
