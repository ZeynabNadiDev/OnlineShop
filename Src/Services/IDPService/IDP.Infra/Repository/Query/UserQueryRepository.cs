using IDP.Domain.Entities;
using IDP.Domain.IRepository.Query;
using IDP.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Infra.Persistence.Repository.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Infra.Repository.Query
{
    public class UserQueryRepository : BaseQueryRepository<User, IDPQueryDbContext>,IUserQueryRepository
    {
        public UserQueryRepository(IDPQueryDbContext context) : base(context)
        {
        }

        public async Task<User?> GetByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.Users
                  .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
        }

    }
}
