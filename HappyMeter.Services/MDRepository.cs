using HappyMeter.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HappyMeter.Services
{
    public class MDRepository<T> where T : Entity, new()
    {
        IMongoDatabase _database;
        IMongoCollection<T> _collection;
        public MDRepository()
        {
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["db"]) || string.IsNullOrEmpty(ConfigurationManager.AppSettings["dbIp"]))
            {
                throw new Exception("Connectionstring to MongoDB is not provided in Web.Config");
            }
            string dbName = ConfigurationManager.AppSettings["db"].ToString();
            string connectionString = ConfigurationManager.AppSettings["dbIp"].ToString();
            var client = new MongoClient(connectionString);

            _database = client.GetDatabase(dbName);
            _collection = _database.GetCollection<T>("faces");
        }

        public bool InsertOne(T insertedObject)
        {
            insertedObject.CreatedAt = DateTime.Now;
            insertedObject.ModifiedAt = DateTime.Now;
            insertedObject.IsDeleted = false;
            _collection.InsertOne(insertedObject);
            return true;
        }

        public Task InsertOneAsync(T insertedObject)
        {

            return _collection.InsertOneAsync(insertedObject);
        }

        public bool InsertMany(List<T> insertedObjects)
        {
            Parallel.ForEach(insertedObjects,
                    (x) =>
                    {
                        x.CreatedAt = DateTime.Now;
                        x.ModifiedAt = DateTime.Now;
                        x.IsDeleted = false;
                    }
                );
            _collection.InsertMany(insertedObjects);
            return true;
        }

        public Task InsertManyAsync(List<T> insertedObjects)
        {
            Parallel.ForEach(insertedObjects,
                    (x) =>
                    {
                        x.CreatedAt = DateTime.Now;
                        x.ModifiedAt = DateTime.Now;
                        x.IsDeleted = false;
                    }
                );
            return _collection.InsertManyAsync(insertedObjects);
        }

        public T GetById(string id)
        {
            //TODO
            return new T();
        }

        public IMongoQueryable<T> GetAll()
        {
            return _collection.AsQueryable<T>();
        }

        public List<T> GetAllList()
        {
            return _collection.AsQueryable<T>().ToList();
        }

        public void Find(Expression<System.Func<T, bool>> filter)
        {
            var result = _collection.Find(filter).ToList();
        }
    }
}
