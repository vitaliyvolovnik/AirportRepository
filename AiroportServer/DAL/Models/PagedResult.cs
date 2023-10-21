using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class PagedResult<TEntity>
    {
        public IEnumerable<TEntity> Result { get; set;}
        public int TotalItems { get; set;}
        public int PageIndex { get; set;}
        public int PageSize { get; set;}
    }
}
