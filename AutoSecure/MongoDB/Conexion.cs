using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSecure.MongoDB
{
    public class MongoDBService
    {
        private readonly IMongoCollection<BsonDocument> _usersCollection;

        public MongoDBService()
        {
            var client = new MongoClient("mongodb+srv://dye:tero@control.nmu0r.mongodb.net/?retryWrites=true&w=majority&appName=Control");
            var database = client.GetDatabase("Sistema");
            _usersCollection = database.GetCollection<BsonDocument>("usuarios");
        }

        public async Task<BsonDocument> AuthenticateUser(string email, string password)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("email", email) & Builders<BsonDocument>.Filter.Eq("password", password);
            return await _usersCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<bool> RegisterUser(string email, string password)
        {
            var existingUser = await _usersCollection.Find(Builders<BsonDocument>.Filter.Eq("email", email)).FirstOrDefaultAsync();
            if (existingUser != null)
                return false;

            var newUser = new BsonDocument { { "email", email }, { "password", password } };
            await _usersCollection.InsertOneAsync(newUser);
            return true;
        }
    }
}
