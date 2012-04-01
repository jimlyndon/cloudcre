using System.Collections.Generic;
using System.Web.Mvc;

namespace Cloudcre.Web.Models
{
    public class PropertySearchResultViewModel
    {
        public int NumberOfTitlesFound { get; set; }
        public int TotalNumberOfPages { get; set; }
        public int CurrentPage { get; set; }
        public IEnumerable<SelectListItem> PropertyType { get; set; }
        public decimal? SqftMinFilter { get; set; }
        public decimal? SqftMaxFilter { get; set; }
        public decimal? SqftMin { get; set; }
        public decimal? SqftMax { get; set; }
    }
}