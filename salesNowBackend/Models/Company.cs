
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace salesNowBackend.Models
{
    [FirestoreData]
    public class Company
    {
        //[Key]

        //[FirestoreProperty]
        //public string CompanyId { get; set; }
        //public string SystemIDOrganisation { get; set; }
        [FirestoreProperty]
        public string Name { get; set; }
        [FirestoreProperty]
        //public string Organisationsnummer { get; set; }
        public string OrganizationNumber { get; set; }
        [FirestoreProperty]

        //public string Telephone { get; set; }
        public string PhoneNumber { get; set; }
        [FirestoreProperty]
        public string Type { get; set; }
        [FirestoreProperty]
        public string Status { get; set; }
        [FirestoreProperty]
        public string Industry { get; set; }
        [FirestoreProperty]
        public string Address { get; set; }
        [FirestoreProperty]
        //public string PostCodeZIP { get; set; }
        public string ZipCode { get; set; }
        [FirestoreProperty]
        public string City { get; set; }
        [FirestoreProperty]
        public string Comment { get; set; }
        [FirestoreProperty]
        public string ToolsAndTechnique { get; set; }
        [FirestoreProperty]
        public string Comments { get; set; }

        //public List<Activity> Activities { get; set; } = new List<Activity>();
        //public List<ContactPerson> ContactPersons { get; set; } = new List<ContactPerson>();
        //public List<BusinessOpportunity> BusinessOpportunities { get; set; } = new List<BusinessOpportunity>();
    }
}
