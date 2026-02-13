using IDP.Domain.DTO;
using IDP.Domain.IRepository.Query;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IDP.Infra.Repository.Query
{
    public class OtpRedisQueryRepository : IOtpRedisQueryRepository
    {
        private readonly IConnectionMultiplexer _redis;

        public OtpRedisQueryRepository(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task<bool> IsValidAsync(string phoneNumber, string otp)
        {
            var db = _redis.GetDatabase();
            var key = $"otp:{phoneNumber}";
            var storedOtp = await db.StringGetAsync(key);
       

            if (!storedOtp.HasValue)
                return false;

            var otpObj = JsonSerializer.Deserialize<Otp>(storedOtp!);

            return otpObj.OtpCode == otp;
            
        }
    }
}
