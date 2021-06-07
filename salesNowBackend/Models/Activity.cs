using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace salesNowBackend.Models
{
    [FirestoreData]
     public  class Activity
    {
      
        //[FirestoreProperty]
        //public string ActivityId { get; set; }
        [FirestoreProperty]
        public string Name { get; set; }
        [FirestoreProperty]
        public string FullName { get; set; }
        [FirestoreProperty]
        public DateTime Date { get; set; }
        [FirestoreProperty]
        public string Action { get; set; }
        [FirestoreProperty]
        public string Type { get; set; }
        [FirestoreProperty]
        public string Status { get; set; }
        [FirestoreProperty]
        public string Description { get; set; }
        [FirestoreProperty]
        public string Comment { get; set; }
        [FirestoreProperty]
        public string Log { get; set; }

        [FirestoreProperty]
        //public string SystemIDOrganisation { get; set; }
        public string CompanyId { get; set; }

        [FirestoreProperty]
        //public string SystemIDPerson { get; set; }
        public string ContactId { get; set; }

    }
}
