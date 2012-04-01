using System;
using System.Linq.Expressions;
using Cloudcre.Model.Core.Querying;

namespace Cloudcre.Model.Specifications
{
    public class MappingBoundaryRangeSpecification : Specification<MultipleFamily>
    {
        public MappingBoundaryRangeSpecification(decimal? southWestLongitude, decimal? northEastLongitude, decimal? southWestLatitude, decimal? northEastLatitude)
        {
            SouthWestLongitude = southWestLongitude;
            NorthEastLongitude = northEastLongitude;
            SouthWestLatitude = southWestLatitude;
            NorthEastLatitude = northEastLatitude;
        }

        public decimal? SouthWestLongitude { get; private set; }
        public decimal? NorthEastLongitude { get; private set; }
        public decimal? SouthWestLatitude { get; private set; }
        public decimal? NorthEastLatitude { get; private set; }

        public override Expression<Func<MultipleFamily, bool>> IsSatisfied()
        {
            Expression<Func<MultipleFamily, bool>> greaterLongitude = x => x.Longitude >= SouthWestLongitude;
            Expression<Func<MultipleFamily, bool>> lessLongitude = x => x.Longitude <= NorthEastLongitude;
            Expression<Func<MultipleFamily, bool>> greaterLatitude = x => x.Latitude >= SouthWestLatitude;
            Expression<Func<MultipleFamily, bool>> lessLatitude = x => x.Latitude <= NorthEastLatitude;

            return greaterLongitude
                .And(lessLongitude)
                .And(greaterLatitude)
                .And(lessLatitude);
        }
    }
}