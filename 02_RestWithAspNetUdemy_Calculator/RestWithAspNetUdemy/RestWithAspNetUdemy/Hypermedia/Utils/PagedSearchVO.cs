using RestWithAspNetUdemy.Hypermedia.Abstract;

namespace RestWithAspNetUdemy.Hypermedia.Utils
{
    public class PagedSearchVO<T> where T : ISupportsHypermedia
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalResults { get; set; }
        public string SortField { get; set; }
        public string SortDirections { get; set; }
        public Dictionary<string, Object> Filters { get; set; }
        public List<T> List { get; set; }

        public PagedSearchVO()
        {

        }

        public PagedSearchVO(int currentPage, int pageSize, string sortField, string sortDirections)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortField = sortField;
            SortDirections = sortDirections;
        }

        public PagedSearchVO(int currentPage, int pageSize, string sortField, string sortDirections, Dictionary<string, object> filters)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortField = sortField;
            SortDirections = sortDirections;
            Filters = filters;
        }

        public PagedSearchVO(int currentPage, string sortField, string sortDirections)
            : this(currentPage, 10, sortField, sortDirections)
        {

        }

        public int GetCurrentPage()
        {
            return CurrentPage == 0 ? 2 : CurrentPage;
        }

        public int GetPageSize()
        {
            return PageSize == 0 ? 10 : PageSize;
        }
    }
}
