using Domain;
using MongoDB.Driver;

namespace Repository
{
    public class IdeasContext
    {
        private readonly IMongoDatabase _database;

        public IdeasContext(string connectionString, string databaseName)
        {
            IMongoClient client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);

        }

        public IMongoCollection<Idea> Ideas
        {
            get
            {
                return _database.GetCollection<Idea>("ideas");
            }
        }
    }
}