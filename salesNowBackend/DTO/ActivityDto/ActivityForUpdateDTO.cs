using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace salesNowBackend.DTO.ActivityDto
{
    public class ActivityForUpdateDTO
    {
        [Required(ErrorMessage = "This Field Is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This Field Is Required")]
        public string FullName { get; set; }

        //  [Required(ErrorMessage = "This Field Is Required")]
        public DateTime? Date { get; set; } = new DateTime();

        //  [Required(ErrorMessage = "This Field Is Required")]
        public string Action { get; set; }

       // [Required(ErrorMessage = "This Field Is Required")]
        public string Type { get; set; }

       // [Required(ErrorMessage = "This Field Is Required")]
        public string Status { get; set; }

      //  [Required(ErrorMessage = "This Field Is Required")]
        public string Description { get; set; }

      //  [Required(ErrorMessage = "This Field Is Required")]
        public string Comment { get; set; }

       // [Required(ErrorMessage = "This Field Is Required")]
        public string Log { get; set; }

        [Required(ErrorMessage = "This Field Is Required")]
        public string CompanyId { get; set; }

        [Required(ErrorMessage = "This Field Is Required")]
        public string ContactId { get; set; }
    }
}
