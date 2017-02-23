using HappyMeter.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace HappyMeter.Services
{
    public class EmotionService : IEmotionService
    {
        public void AddEmotion(InfoDTO infoDTO)
        {
            string dbName = ConfigurationManager.AppSettings["db"].ToString();
            string connectionString = ConfigurationManager.AppSettings["dbIp"].ToString();
            var client = new MongoClient(connectionString);

            //Use the MongoClient to access the server
            var database = client.GetDatabase(dbName);
            var collection = database.GetCollection<InfoDTO>("faces");
            collection.InsertOne(infoDTO);
        }

        public List<InfoDTO> GetEmotionsPerCategory(string category)
        {
            string db = ConfigurationManager.AppSettings["db"].ToString();
            string connectionString = ConfigurationManager.AppSettings["dbIp"].ToString();
            // Create a MongoClient object by using the connection string
            var client = new MongoClient(connectionString);

            //Use the MongoClient to access the server
            var database = client.GetDatabase(db);
            var collection = database.GetCollection<InfoDTO>("faces");
            var values = collection.AsQueryable<InfoDTO>().ToList();
            return values;
        }


    }
}
