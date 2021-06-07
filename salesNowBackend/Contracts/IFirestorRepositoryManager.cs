using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;

namespace salesNowBackend.Contracts
{
   public interface IFirestorRepositoryManager
    {
        FirestoreDb Db { get;}
       
    }
}
