using salesNowBackend.Contracts;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace salesNowBackend.FirestoreRepository
{
    public class FirestoreBase<T> : IFirestoreBase<T> where T : class
    {

        private readonly IFirestorRepositoryManager _firestor;
        public FirestoreBase(IFirestorRepositoryManager firestor)
        {
            _firestor = firestor;
        }



        public async Task<string> Delete(string collectionName,string id)
        {
            var document = await _firestor.Db.Collection(collectionName).Document(id).GetSnapshotAsync();
            if (document.Exists)
            {
                await document.Reference.DeleteAsync(); //await _firestor.Db.Collection("Companies").Document(id).DeleteAsync();

                return id;
            }
            return null;

        }

        public async Task<QuerySnapshot> FindAll(string collectionName){

            
           var documentSnapshots =  await _firestor.Db.Collection(collectionName).GetSnapshotAsync();
            if (documentSnapshots.Documents.Count > 0)
            {
                return documentSnapshots;
            }
            return null;

        }


        public IQueryable<T> FindByCondation(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Create(string collectionName, string companyId,string subCollectionName ,T entity)
        {
            var document = _firestor.Db.Collection(collectionName).Document(companyId);

            var addDocument = await document.Collection(subCollectionName).AddAsync(entity);

            if (addDocument.Id != null)
            {
               
                await _firestor.Db.Collection(subCollectionName).Document(addDocument.Id).SetAsync(entity);
                return addDocument.Id;
            }

            return null;
        }
        public async Task<string> Update(string collectionName, string companyId, string subCollectionName, string id, T entity)
        {
            var document = await _firestor.Db.Collection(collectionName).Document(companyId).Collection(subCollectionName).Document(id).GetSnapshotAsync();

            if (document.Exists)
            {
                var doccumentConvert = JsonConvert.SerializeObject(entity);
                var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(doccumentConvert);

                await _firestor.Db.Collection(collectionName).Document(companyId).Collection(subCollectionName).Document(id).UpdateAsync(json);

              
                await _firestor.Db.Collection(subCollectionName).Document(id).UpdateAsync(json);
              

                return id;
            }

            return null;
        }
        public async Task<string> Delete(string collectionName, string companyId, string subCollectionName, string id)
        {
            var document = await _firestor.Db.Collection(collectionName).Document(companyId).Collection(subCollectionName).Document(id).DeleteAsync();

            var documentInActivityCollection = await _firestor.Db.Collection(subCollectionName).Document(id).DeleteAsync();

            var documentDeleted = await _firestor.Db.Collection(collectionName).Document(companyId).Collection(subCollectionName).Document(id).GetSnapshotAsync();

            if (!documentDeleted.Exists)
            {
                return id;
            }
            return null;
        }

    }
    
}
