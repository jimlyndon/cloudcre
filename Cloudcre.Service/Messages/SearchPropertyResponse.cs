using System.Collections.Generic;
using Cloudcre.Model;

namespace Cloudcre.Service.Messages
{        
    public class SearchPropertyResponse<TVm> : ResponseBase
    {
        public IEnumerable<TVm> Properties { get; set; }
        public PropertyType PropertyType { get; set; }
        public int NumberOfTitlesFound { get; set; }
        public int TotalNumberOfPages { get; set; }
        public int CurrentPage { get; set; }
        public decimal? SqftMinFilter { get; set; }
        public decimal? SqftMaxFilter { get; set; }
        public decimal? SqftMin { get; set; }
        public decimal? SqftMax { get; set; }
    }
}