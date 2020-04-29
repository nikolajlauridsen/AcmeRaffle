using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace PaginatedList
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);
            this.AddRange(items);
        }

        public bool HasPreviousPage => (PageIndex > 1);

        public bool HasNextPage => (PageIndex < TotalPages);

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            // Find the amount of items in the source
            int count = await new Task<int>(source.Count);
            // Skip to the desired page and take a page
            List<T> items = await new Task<List<T>>((() => 
                source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList()));
            
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
