using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnApi.Models
{
    public class Pagination<T> : List<T>
    {
        private Task<int> count;
        private int pagesize;

        public int PageIndex { get; private set; }

        public int TotalPages { get; set; }

        public Pagination(List<T> items, int count, int pageindex, int pagesize)
        {
            PageIndex = pageindex;
            TotalPages = (int)Math.Ceiling(count / (double)pagesize);
            AddRange(items);
        }

        public Pagination(IEnumerable<T> collection, Task<int> count, int pageindex, int pagesize) : base(collection)
        {
            this.count = count;
            PageIndex = pageindex;
            this.pagesize = pagesize;
        }

        public bool PreviousPage
        {
            get 
            {
                return (PageIndex > 1);
            }
        }

        public bool NextPage
        {
            get 
            {
                return (PageIndex < TotalPages);
            }
        }

        public static async Task<Pagination<T>> CreateAsync(IQueryable<T> source, int pageindex, int pagesize)
        {
           var count = await source.CountAsync();

            var items = source.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            return new Pagination<T>(items, count, pageindex, pagesize);
        }
    }  
}
