using Microsoft.EntityFrameworkCore;
using Shared.Domain.Interface.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Infra.Persistence.UOW
{
    public class UnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext
    {
        private readonly TContext _context;
        public UnitOfWork(TContext context)
        {
            _context = context;
        }
        public async Task<int> SaveChangeAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
