using System;

namespace Cloudcre.Model.Core.Querying
{
    [Flags]
    public enum SeachQueryParameters
    {
        ParcelId = 1,
        Name = 2,
        AddressLine1 = 4,
        City = 8,
        Zip = 16,
        County = 32
    }

    public class SeachQuery
    {
        public string Query { get; set; }
        public SeachQueryParameters SearchFields { get; set; }
        public bool WildCard { get; set; }
        public bool TokenizeQuery { get; set; }
    }
}