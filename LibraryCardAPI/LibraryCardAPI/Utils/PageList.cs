using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCardAPI.Utils
{
    public class PageList<T>
    {
       
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalResults { get; set; }
        public string SortFields { get; set; }
        public string SortDirections { get; set; }
        public Dictionary<string, Object> Filters { get; set; }
        public List<T> List { get; set; }

        public PageList(){}

        public PageList(int currentPage, int pageSize, string sortFields, string sortDirections)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortFields = sortFields;
            SortDirections = sortDirections;
        }

        public PageList(int currentPage, int pageSize, string sortFields, string sortDirections, Dictionary<string, object> filters) : this(currentPage, pageSize, sortFields, sortDirections)
        {
            Filters = filters;
        }

        public PageList(int currentPage, string sortFields, string sortDirections) : this(currentPage, 10, sortFields, sortDirections){}

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
