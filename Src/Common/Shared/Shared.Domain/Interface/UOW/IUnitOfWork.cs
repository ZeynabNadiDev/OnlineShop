using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Interface.UOW
{
    public interface IUnitOfWork:IDisposable
    {
        Task<int> SaveChangeAsync(CancellationToken cancellationToken);
    }
}
