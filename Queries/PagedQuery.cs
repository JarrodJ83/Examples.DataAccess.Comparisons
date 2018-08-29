namespace Queries
{
    public abstract class PagedQuery<TResult> : IQueryDef<TResult>
    {
        /// <summary>
        /// Number of records to skip in the sorted query
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Number of records to take in the sorted query
        /// </summary>
        public int PageSize { get; set; }

        public PagedQuery(int offset, int pageSize)
        {
            Offset = offset;
            PageSize = pageSize;
        }
    }
}