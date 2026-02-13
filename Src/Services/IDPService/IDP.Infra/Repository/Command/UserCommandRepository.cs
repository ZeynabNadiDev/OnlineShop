using Azure.Core;
using IDP.Domain.Entities;
using IDP.Domain.IRepository.Command;
using IDP.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Infra.Persistence.Repository.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Infra.Repository.Command
{
    public class UserCommandRepository : BaseCommandRepository<User, ShopDBContext>,IUserCommandRepository
    {
        public UserCommandRepository(ShopDBContext context) : base(context)
        {
        }
        
        
    }
}
