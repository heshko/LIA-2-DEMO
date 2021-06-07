
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace salesNowBackend.Models
{
    [FirestoreData]
    public  class ContactPerson
    {
   
        //[FirestoreProperty]
        ////public string SystemIDPerson { get; set; }
        //public string ContactId { get; set; }

        [FirestoreProperty]
        public string Name { get; set; }
        [FirestoreProperty]
        public string FirstName { get; set; }
        [FirestoreProperty]
        public string LastName { get; set; }
        [FirestoreProperty]
        public string Status { get; set; }
        [FirestoreProperty]
        public string JobTitle { get; set; }
        [FirestoreProperty]
        public string Email { get; set; }
        [FirestoreProperty]
        public string DirectTelephone { get; set; }
        [FirestoreProperty]
        public string Mandate { get; set; }
        [FirestoreProperty]
        public string PrimaryContact { get; set; }
        [FirestoreProperty]
        public string Comment { get; set; }
        [FirestoreProperty]
        public string Log { get; set; }

        [FirestoreProperty]
        //public string SystemIDOrganisation { get; set; }
        public string CompanyId { get; set; }

    }
}
