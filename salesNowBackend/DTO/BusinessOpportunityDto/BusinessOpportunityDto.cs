using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace salesNowBackend.DTO.BusinessOpportunityDto
{
    public class BusinessOpportunityDTO
    {
        public string BussinessId { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public double Revenue { get; set; }
        public string PipelineText { get; set; }
        public string PipelinePct { get; set; }
        public string QuotaInformation { get; set; }
        public string ReasonLost { get; set; }
        public string Comment { get; set; }
        public string Log { get; set; }
        //public string SystemIDOrganisation { get; set; }
        public string CompanyId { get; set; }
        //public string SystemIDPerson { get; set; }
        public string ContactId { get; set; }

    }
}
