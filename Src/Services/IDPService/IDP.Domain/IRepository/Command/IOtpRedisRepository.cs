using IDP.Domain.DTO;
using Shared.Domain.Interface.Repository.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Domain.IRepository.Command
{
    public interface IOtpRedisRepository
    {
        Task<bool> AddAsync(Otp otp);
        Task<bool> DeleteAsync(Otp otp);
    }
}
