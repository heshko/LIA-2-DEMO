
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace salesNowBackend.Models
{
    [FirestoreData]
    public class BusinessOpportunity
    {
        //[Key]
        //[FirestoreProperty]
        //public string BussinessId { get; set; }
        [FirestoreProperty]
        public string Name { get; set; }
        [FirestoreProperty]

        public string FullName { get; set; }
        [FirestoreProperty]

        public string Number { get; set; }
        [FirestoreProperty]
        public string Description { get; set; }
        [FirestoreProperty]
        public DateTime Created { get; set; }
        [FirestoreProperty]
        public double Revenue { get; set; }
        [FirestoreProperty]
        public string PipelineText { get; set; }
        [FirestoreProperty]
        public string PipelinePct { get; set; }
        [FirestoreProperty]
        public string QuotaInformation { get; set; }
        [FirestoreProperty]
        public string ReasonLost { get; set; }
        [FirestoreProperty]
        public string Comment { get; set; }
        [FirestoreProperty]
        public string Log { get; set; }

        [FirestoreProperty]
        //public string SystemIDOrganisation { get; set; }
        public string CompanyId { get; set; }
        [FirestoreProperty]
        // public string SystemIDPerson { get; set; }
        public string ContactId { get; set; }

    }
}
