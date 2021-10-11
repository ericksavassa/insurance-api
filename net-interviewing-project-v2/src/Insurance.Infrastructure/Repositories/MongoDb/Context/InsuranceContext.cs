using Insurance.Domain.Entities;
using Insurance.Infrastructure.Repositories.MongoDb.Dtos;
using MongoDB.Driver;

namespace Insurance.Infrastructure.Repositories.MongoDb.Context
{
    public class InsuranceContext : IInsuranceContext
    {
        private readonly IMongoDatabase Db;

        public InsuranceContext(MongoDBConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            this.Db = client.GetDatabase(config.Database);
        }

        public IMongoCollection<Surcharge> Surcharges => this.Db.GetCollection<Surcharge>("Surcharges");
    }
}
