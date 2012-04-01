using NHibernate.Search.Attributes;

namespace Cloudcre.Model
{
    [Indexed]
    public class Address
    {
        [Field(Index.Tokenized)]
        public virtual string AddressLine1 { get; set; }

        [Field(Index.Tokenized)]
        public virtual string AddressLine2 { get; set; }

        [Field(Index.Tokenized)]
        public virtual string City { get; set; }

        [Field(Index.Tokenized)]
        public virtual string County { get; set; }

        [Field(Index.Tokenized)]
        public virtual string StateProvinceRegion { get; set; }

        [Field(Index.Tokenized)]
        public virtual string Zip { get; set; }

        [Field(Index.Tokenized)]
        public virtual string MetropolitanStatisticalArea { get; set; }

        //protected override void Validate()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
