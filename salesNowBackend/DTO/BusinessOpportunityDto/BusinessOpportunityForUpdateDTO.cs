using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace salesNowBackend.DTO.BusinessOpportunityDto
{
    public class BusinessOpportunityForUpdateDTO
    {
        [Required(ErrorMessage = "This Field Is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This Field Is Required")]
        public string FullName { get; set; }

       // [Required(ErrorMessage = "This Field Is Required")]
        public string Number { get; set; }

        [Required(ErrorMessage = "This Field Is Required")]
        public string Description { get; set; }

        // [Required(ErrorMessage = "This Field Is Required")]
         public DateTime? Created { get; set; } = new DateTime();

        //[Required(ErrorMessage = "This Field Is Required")]
        public double Revenue { get; set; }

        //[Required(ErrorMessage = "This Field Is Required")]
        public string PipelineText { get; set; }

       // [Required(ErrorMessage = "This Field Is Required")]
        public string PipelinePct { get; set; }

        //[Required(ErrorMessage = "This Field Is Required")]
        public string QuotaInformation { get; set; }

        //[Required(ErrorMessage = "This Field Is Required")]
        public string ReasonLost { get; set; }

        //[Required(ErrorMessage = "This Field Is Required")]
        public string Comment { get; set; }
        
       // [Required(ErrorMessage = "This Field Is Required")]
         public string Log { get; set; }

        [Required(ErrorMessage = "This Field Is Required")]
        public string CompanyId { get; set; }

        [Required(ErrorMessage = "This Field Is Required")]
        public string ContactId { get; set; }

    }
}
