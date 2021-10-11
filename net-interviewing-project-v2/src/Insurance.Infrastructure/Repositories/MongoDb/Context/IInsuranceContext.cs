using Insurance.Domain.Entities;
using MongoDB.Driver;

namespace Insurance.Infrastructure.Repositories.MongoDb.Context
{
    public interface IInsuranceContext
    {
        IMongoCollection<Surcharge> Surcharges { get; }
    }
}
