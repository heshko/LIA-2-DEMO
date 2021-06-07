using salesNowBackend.Contracts;
using Google.Cloud.Firestore;
using System;
using salesNowBackend.FirestoreRepository;
using System.IO;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace salesNowBackend.FirestoreRepository
{
   
    public class FireStoreRepossitoryManager : IFirestorRepositoryManager
    {
        private  FirestoreDb _db ;

        public FirestoreDb Db { get
            {
                if (_db is null)
                {
                    //var fullPath = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location))
                    //   .FirstOrDefault(x => x.EndsWith("geshdo.json", StringComparison.OrdinalIgnoreCase));
                    //Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "fullPath");
                    FirestoreDb db = FirestoreDb.Create("geshdo-salesnow-dev");
                        _db = db;
                }
                return _db;
            } 
        }

       
    }
}
