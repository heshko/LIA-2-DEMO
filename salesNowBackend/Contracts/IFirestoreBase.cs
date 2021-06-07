using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace salesNowBackend.Contracts
{
    public  interface IFirestoreBase<T>
    {

        //Task<string> Create(T entity,string collectionName);
        //Task<string> Delete(string collectionname,string id);
        //void Update(T entity);
        Task<string> Delete(string collectionName, string companyId, string subCollectionName, string id);
        Task<string> Update(string collectionName, string companyId, string subCollectionName, string id, T entity);
        Task<QuerySnapshot> FindAll( string collectionName);
        IQueryable<T> FindByCondation(Expression<Func<T,bool>>  expression);
        Task<string> Create(string collectionName, string companyId, string subCollectionName, T entity);
    } 
}
