using System;
using System.Collections.Generic;
using System.Linq;
using Cloudcre.Model.Core.Querying;
using System.Linq.Expressions;

namespace Cloudcre.Model.Specifications
{
    public class PropertySquareFootRangeSpecification : Specification<MultipleFamily>
    {
        public PropertySquareFootRangeSpecification(decimal? sqftMinFilter, decimal? sqftMaxFilter)
        {
            SqftMinFilter = sqftMinFilter;
            SqftMaxFilter = sqftMaxFilter;
        }

        public decimal? SqftMinFilter { get; private set; }
        public decimal? SqftMaxFilter { get; private set; }

        public override Expression<Func<MultipleFamily, bool>> IsSatisfied()
        {
            Expression<Func<MultipleFamily, bool>> max = p => p.BuildingTotalSquareFoot <= SqftMaxFilter;
            Expression<Func<MultipleFamily, bool>> min = p => p.BuildingTotalSquareFoot >= SqftMinFilter;
            return max.And(min);
        }
    }
}