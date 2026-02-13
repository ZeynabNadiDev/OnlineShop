using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Model
{
    public class Pagenation<T>
    {
        public int PageIndex { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public IReadOnlyList<T> Items { get; }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public Pagenation(IReadOnlyList<T> items, int count, int pageIndex, int pageSize)
        {
            Items = items;
            TotalCount = count;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
