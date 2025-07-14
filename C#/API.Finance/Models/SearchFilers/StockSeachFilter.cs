using Microsoft.AspNetCore.Http.HttpResults;

namespace API.Models.SearchFilers
{
    /// <summary>
    /// Search filer for stocks or companies.
    /// </summary>
    public class StockSeachFilter
    {
        /// <summary>
        /// Default: true. Only show active stocks. When set to 'false' ONLY 'inactive' stocks will be provided.
        /// </summary>
        public bool? ShowInactive { get; set; } = null;


        /// <summary>
        /// Default: false. Show comments related to this Stock the associated object. 
        /// </summary>
        public bool? ShowComments { get; set; } = null;

        /// <summary>
        /// Search stocks by ticker symbol.
        /// </summary>
        public string? Symbol { get; set; } = null;

        /// <summary>
        /// Search stocks by company name.
        /// </summary>
        public string? CompanyName { get; set; } = null;


        /// <summary>Metric value of sort order result display set.</summary>
        public string? SortBy { get; set; } = null;

        /// <summary>Ordinal directional value of result display set.</summary>
        public bool IsDescending { get; set; } = true;


        /// <summary>Number of reults to return.</summary>
        public int PageSize { get; set; } = 20;

        /// <summary>Paginated return of result set.</summary>
        public int PageNumber { get; set; } = 1;
    }
}
