using IDP.Domain.DTO;
using Shared.Domain.Interface.Repository.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Domain.IRepository.Query
{
    public interface IOtpRedisQueryRepository
    {
        Task<bool> IsValidAsync(string phoneNumber, string otp);
        
    }
}
