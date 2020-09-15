using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopProject3.Domain.Helpers
{
    public class PageList<T>:List<T>
    {
        public int CurrentPage{ get; private set; }

        public int TotalPages { get; private set; }

        public int PageSize { get; private set; }

        public int TotalCount { get; private set; }

        public bool HasPrevious => (CurrentPage > 1);
        public bool HasNext => (CurrentPage < TotalPages);

        public PageList(List<T> items, int count,int PageNumber, int PageSize)
        {
            TotalCount = count;
            PageSize = PageSize;
            CurrentPage = PageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            AddRange(items);
        }
        public static PageList<T> Create(IQueryable<T> source,int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PageList<T>(items, count, pageNumber, pageSize);
        }
    }
}
