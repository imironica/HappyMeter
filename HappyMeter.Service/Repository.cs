using HappyMeter.Model;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HappyMeter.Service
{
    public class Repository<T> where T : Entity, new()
    {
        private static readonly ConnectionPolicy ConnectionPolicy = new ConnectionPolicy
        {
            ConnectionMode = ConnectionMode.Direct,
            ConnectionProtocol = Protocol.Tcp,
            RequestTimeout = new TimeSpan(1, 0, 0),
            MaxConnectionLimit = 1000,
            RetryOptions = new RetryOptions
            {
                MaxRetryAttemptsOnThrottledRequests = 10,
                MaxRetryWaitTimeInSeconds = 60
            }
        };

        private string _db;
        private string _endpointUri;
        private string _primaryKey;
        public Repository()
        {
            _db = "happy-meter";
            _endpointUri = "https://happymeter.documents.azure.com:443/";
            _primaryKey = "2zFH2PKIMm9kPp84PTvQKU7RqW18eToauaNuoXQs6UlBJZlFTpJc0tryVZxKNZDv2xFyWr6unkeFrtCHJZzegw==";
        }

        public bool InsertOne(T insertedObject)
        {
            //TODO
            return true;
        }

        public Task InsertOneAsync(T insertedObject)
        {
            //TODO
            return null;
        }

        public bool InsertMany(List<T> insertedObjects)
        {
            //TODO
            return true;
        }

        public Task InsertManyAsync(List<T> insertedObjects)
        {
            //TODO
            return null;
        }

        public T GetById(string id)
        {
            //TODO
            return new T();
        }
 
        public async Task<IEnumerable<T>> FindMultiple(string sqlQuery, string collection)
        {
            DocumentClient client;
            var lstResults = new List<T>();
            using (client = new DocumentClient(new Uri(_endpointUri), _primaryKey, ConnectionPolicy))
            {
                var query = client.CreateDocumentQuery(UriFactory.CreateDocumentCollectionUri(_db, collection), sqlQuery).AsDocumentQuery();
                while (query.HasMoreResults)
                {
                    var documents = await query.ExecuteNextAsync();

                    foreach (var document in documents)
                    {
                        lstResults.Add((T)document);
                    }
                }
            }
            return lstResults;
        }

        public async Task<IEnumerable<dynamic>> FindMultipleDynamic(string sqlQuery, string collection)
        {
            DocumentClient client;
            var lstResults = new List<dynamic>();
            using (client = new DocumentClient(new Uri(_endpointUri), _primaryKey, ConnectionPolicy))
            {
                var query = client.CreateDocumentQuery(UriFactory.CreateDocumentCollectionUri(_db, collection), sqlQuery).AsDocumentQuery();
                while (query.HasMoreResults)
                {
                    var documents = await query.ExecuteNextAsync();

                    foreach (var document in documents)
                    {
                        lstResults.Add(document);
                    }
                }
            }
            return lstResults;
        }

        public async Task<T> FindOne(string sqlQuery, string collection)
        {
            DocumentClient client;
            var lstResults = new List<T>();
            using (client = new DocumentClient(new Uri(_endpointUri), _primaryKey, ConnectionPolicy))
            {
                var query = client.CreateDocumentQuery(UriFactory.CreateDocumentCollectionUri(_db, collection), sqlQuery).AsDocumentQuery();
                while (query.HasMoreResults)
                {
                    var documents = await query.ExecuteNextAsync();

                    foreach (var document in documents)
                    {
                        return document;
                    }
                }
            }
            return new T();
        }
    }
}
