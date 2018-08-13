using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    public class PagedData<TData> 
    {
        public int Offset { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public TData[] Data { get; set; }
    }
}
