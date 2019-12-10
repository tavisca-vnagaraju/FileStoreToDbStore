using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;

namespace FileStoreToDbStore
{
    public class MongoDbStore : IDbStore, IFiles
    {
        private IMongoDatabase _database;
        private string[] _lines;
        public void ConnectToDatabase(string name)
        {
            MongoClient dbClient = new MongoClient("mongodb://127.0.0.1:27017");
            _database = dbClient.GetDatabase(name);
        }
        public void InsertData()
        {
            var collection = _database.GetCollection<BsonDocument>("coordinates");
            List<BsonDocument> bsonDocuments = new List<BsonDocument>();
            string[] keys = _lines[0].Split("|");
            for (int index = 1; index < _lines.Length; index++)
            {
                BsonDocument bsonDocument = new BsonDocument();
                var rowValues = _lines[index].Split("|");
                for (int j = 0; j < rowValues.Length; j++)
                {
                    var bsonElement = new BsonElement(keys[j], rowValues[j]);
                    bsonDocument.Add(bsonElement);
                }
                bsonDocuments.Add(bsonDocument);
            }
            collection.InsertMany(bsonDocuments);
        }

        public void ReadFile(string path)
        {
            _lines = File.ReadAllLines(path);
        }
    }

}
