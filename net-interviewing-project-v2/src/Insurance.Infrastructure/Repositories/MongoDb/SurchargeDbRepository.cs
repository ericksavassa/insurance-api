using Insurance.Domain.Entities;
using Insurance.Domain.Interfaces.Repositories;
using Insurance.Infrastructure.Repositories.MongoDb.Context;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Insurance.Infrastructure.Repositories.MongoDb
{
    public class SurchargeDbRepository : ISurchargeRepository
    {

        private readonly IInsuranceContext _context;

        public SurchargeDbRepository(IInsuranceContext context)
        {
            _context = context;
        }

        public async Task Create(Surcharge surcharge)
        {
            try
            {
                await _context.Surcharges.InsertOneAsync(surcharge);
            }
            catch (Exception)
            {
                throw new Exception("Error trying to save data into mongoDb!");
            }
        }

        public async Task<bool> Update(Surcharge surcharge)
        {
            try
            {
                ReplaceOneResult updateResult = await _context
                       .Surcharges
                       .ReplaceOneAsync(
                           filter: g => g.ProductTypeId == surcharge.ProductTypeId,
                           replacement: surcharge);
                return updateResult.IsAcknowledged
                        && updateResult.ModifiedCount > 0;
            }
            catch (Exception)
            {
                throw new Exception("Error trying to save data into mongoDb!");
            }
        }

        public async Task<Surcharge> GetByProductTypeId(int productTypeId)
        {
            try
            {
                Expression<Func<Surcharge, bool>> filter = x => x.ProductTypeId.Equals(productTypeId);
                Surcharge surcharge = await _context.Surcharges.Find<Surcharge>(filter).FirstOrDefaultAsync();
                return surcharge;
            }
            catch
            {
                throw new Exception("Error trying to get data from mongoDb!");
            }
        }
    }
}
