using Insurance.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace Insurance.Infrastructure.Repositories.MongoDb.Dtos
{
    public static class SurchargeMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Surcharge>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdField(x => x.Id).SetIdGenerator(StringObjectIdGenerator.Instance).SetSerializer(new StringSerializer(BsonType.ObjectId));
                map.MapMember(x => x.ProductTypeId).SetIsRequired(true);
                map.MapMember(x => x.SurchargeRate).SetIsRequired(true);
            });
        }
    }
}
