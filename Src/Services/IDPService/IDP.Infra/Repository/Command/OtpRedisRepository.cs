
using IDP.Domain.DTO;
using IDP.Domain.IRepository.Command;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shared.Domain.Interface.Repository.Command;
using Shared.Infra.Persistence.Repository.Command;
using StackExchange.Redis;



namespace IDP.Infra.Repository.Command
{
    public class OtpRedisRepository : IOtpRedisRepository
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IConfiguration _configuration;

        public OtpRedisRepository(
            IConnectionMultiplexer redis,
            IConfiguration configuration)
        {
            _redis = redis;
            _configuration = configuration;
        }

        public async Task<bool> AddAsync(Otp entity)
        {
            var db = _redis.GetDatabase();

            int time = Convert.ToInt32(
                _configuration["Otp:OtpTime"]
            );

            var key = GetKey(entity.MobileNumber);

            await db.StringSetAsync(
                key,
                JsonConvert.SerializeObject(entity),
                TimeSpan.FromMinutes(time)
            );

            return true;
        }

        public async Task<bool> DeleteAsync(Otp entity)
        {
            var db = _redis.GetDatabase();
            await db.KeyDeleteAsync(GetKey(entity.MobileNumber));
            return true;
        }

        private static string GetKey(string mobileNumber)
            => $"otp:{mobileNumber}";
    }


}
