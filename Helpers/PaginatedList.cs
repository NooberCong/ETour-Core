using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public class PaginatedList<T> : List<T>
    {
        public PaginationInfo PaginationInfo { get; set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PaginationInfo = new PaginationInfo
            {
                PageIndex = pageIndex,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };

            this.AddRange(items);
        }

        public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }


    }
    public class PaginationInfo
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }
    }
}
