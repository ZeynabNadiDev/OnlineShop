using IDP.Domain.Entities;
using Shared.Domain.Interface.Repository.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Domain.IRepository.Query
{
    public interface IUserQueryRepository:IBaseQueryRepository<User>
    {
        Task<User?> GetByPhoneNumberAsync(string phoneNumber);
    }
}
