using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Cloudcre.Model;
using Cloudcre.Model.Core.Querying;
using Cloudcre.Model.Core.UnitOfWork;
using Cloudcre.Service.Messages;
using Cloudcre.Service.ViewModels;
using Cloudcre.Service.Mapping;

namespace Cloudcre.Service
{
    public abstract class PropertyBaseService<T, TId, TVm>
        where T : Property
        where TVm : PropertyViewModel
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<T, TId> _repository;

        protected IUnitOfWork UnitOfWork { get { return _unitOfWork; } }

        protected IRepository<T, TId> Repository { get { return _repository; } }

        protected PropertyBaseService(IRepository<T, TId> repository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public AddPropertyResponse AddProperty(AddPropertyRequest<TVm> request)
        {
            var response = new AddPropertyResponse();

            try
            {
                T property = request.ViewModel.ConvertToModel<T, TVm>();
                Repository.Save(property);
                UnitOfWork.Commit();
                response.Success = true;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "There was a problem creating this property sale record";
                // TODO: log exception
            }

            return response;
        }

        public RemovePropertyResponse RemoveProperty(RemovePropertyRequest<TId> request)
        {
            var response = new RemovePropertyResponse();

            try
            {
                T apartment = Repository.FindBy(request.Id);
                Repository.Remove(apartment);
                UnitOfWork.Commit();
                response.Success = true;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "There was a problem removing this property sale record";
                // TODO: log exception
            }

            return response;
        }

        public GetPropertyResponse<TVm> GetProperty(GetPropertyRequest<TId> request)
        {
            var response = new GetPropertyResponse<TVm>();

            try
            {
                T property = Repository.FindBy(request.Id);
                response.ViewModel = property.ConvertToViewModel<T, TVm>();
                response.Success = true;
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "There was a problem removing this property sale record";
                // TODO: log exception
            }

            return response;
        }

        public GetPropertiesResponse<TVm> GetProperties(GetPropertiesRequest request)
        {
            var response = new GetPropertiesResponse<TVm>();

            if (request.Ids == null)
            {
                response.Success = false;
                return response;
            }

            var properties = from propertyRepo in Repository.FindAll()
                             join ids in request.Ids on propertyRepo.Id equals ids
                             select propertyRepo;

            response.ViewModels = properties.ConvertToViewModels<T, TVm>()
                .OrderByDescending(x => x.SaleDate)
                .ThenByDescending(x => x.ContractDate)
                .ThenByDescending(x => x.ListingDate);

            response.Success = true;

            return response;
        }

        public virtual SearchPropertyResponse<TVm> SearchProperties(SearchPropertyRequest request)
        {
            return SearchProperties(request, null);
        }

        public SearchPropertyResponse<TVm> SearchProperties(SearchPropertyRequest request, Func<IEnumerable<T>, SearchPropertyRequest, IEnumerable<T>> specification)
        {
            // execute the query and build the resulting properties
            IEnumerable<T> propertiesMatchingQuery = SearchForPropertiesMatchingQuery(request);

            if (specification != null)
                propertiesMatchingQuery = specification(propertiesMatchingQuery, request);
            //propertiesMatchingQuery = FilterPropertiesByTotalSqFtRange(propertiesMatchingQuery, request);

            propertiesMatchingQuery = FilterPropertiesBySaleDateRange(propertiesMatchingQuery, request);

            propertiesMatchingQuery = FilterPropertiesByMappingBoundaries(propertiesMatchingQuery, request);

            propertiesMatchingQuery = SortProperties(propertiesMatchingQuery, request);

            // use resulting properties to build the response
            SearchPropertyResponse<TVm> propertyResponse = propertiesMatchingQuery.CreatePropertiesSearchResultFrom<T, TVm>(request);
            propertyResponse.Success = true;
            return propertyResponse;
        }

        public LocationsResponse GetDistinctListofLocations(LocationsRequest request)
        {
            var response = new LocationsResponse
            {
                Locations = new List<LocationViewModel>()
            };

            IEnumerable<T> properties = Repository.FindAll().ToList();

            foreach (var location in properties
                .Where(x => !(string.IsNullOrEmpty(x.Address.City)) && x.Address.City.ToLower().Contains(request.Term.ToLower()))
                .OrderBy(x => x.Address.City)
                .Select(x => x.Address.City).Distinct())
            {
                response.Locations.Add(new LocationViewModel(location, "city"));
            }

            foreach (var location in properties
                .Where(x => !(string.IsNullOrEmpty(x.Address.County)) && x.Address.County.ToLower().Contains(request.Term.ToLower()))
                .OrderBy(x => x.Address.County)
                .Select(x => x.Address.County).Distinct())
            {
                response.Locations.Add(new LocationViewModel(location, "county"));
            }

            foreach (var location in properties
                .Where(x => !(string.IsNullOrEmpty(x.Address.Zip)) && x.Address.Zip.ToLower().Contains(request.Term.ToLower()))
                .OrderBy(x => x.Address.Zip)
                .Select(x => x.Address.Zip.Substring(0, 5)).Distinct())
            {
                response.Locations.Add(new LocationViewModel(location, "zip"));
            }

            response.Success = true;

            return response;
        }

        protected IEnumerable<T> SearchForPropertiesMatchingQuery(SearchPropertyRequest request)
        {
            //PropertySquareFootRangeSpecification sqftRangeSpec = new PropertySquareFootRangeSpecification(request.SqftMinFilter, request.SqftMaxFilter);

            //MappingBoundaryRangeSpecification mapBoundaryRangeSpec 
            //    = new MappingBoundaryRangeSpecification(
            //        request.MappingBoundary.SouthWest.Longitude, request.MappingBoundary.NorthEast.Longitude,
            //        request.MappingBoundary.SouthWest.Latitude, request.MappingBoundary.NorthEast.Latitude);

            //IEnumerable<Apartment> productsMatchingRefinement = string.IsNullOrEmpty(request.Query) ?
            //    _apartmentRepository.FindAll() : _apartmentRepository.FindBy(sqftRangeSpec.And(mapBoundaryRangeSpec), request.Query, productFields);

            //return productsMatchingRefinement;

            var queries = new List<SeachQuery>();

            if (!string.IsNullOrEmpty(request.Query))
            {
                var keyWordQuery = new SeachQuery
                {
                    Query = request.Query,
                    WildCard = true,
                    TokenizeQuery = true,
                    SearchFields =
                        SeachQueryParameters.ParcelId |
                        SeachQueryParameters.Name |
                        SeachQueryParameters.City |
                        SeachQueryParameters.County |
                        SeachQueryParameters.AddressLine1 |
                        SeachQueryParameters.Zip
                };

                queries.Add(keyWordQuery);
            }

            if (request.LocationQueries != null)
            {
                queries.AddRange(from location in request.LocationQueries
                                 let category = (SeachQueryParameters)Enum.Parse(typeof(SeachQueryParameters), CultureInfo.CurrentCulture.TextInfo.ToTitleCase(location.Category))
                                 select new SeachQuery
                                 {
                                     Query = location.Query,
                                     WildCard = category == SeachQueryParameters.Zip,
                                     TokenizeQuery = false,
                                     SearchFields = category
                                 });
            }

            IEnumerable<T> productsMatchingRefinement =
                (queries.Count < 1)
                ? Repository.FindAll()
                : Repository.FindBy(queries.ToArray());

            return productsMatchingRefinement;
        }

        protected IEnumerable<T> SortProperties(IEnumerable<T> productsMatchingRefinement, SearchPropertyRequest request)
        {
            switch (request.SortBy)
            {
                case PropertiesSortBy.City:
                    productsMatchingRefinement = productsMatchingRefinement.OrderBy(p => p.Address.City);
                    break;
                case PropertiesSortBy.State:
                    productsMatchingRefinement = productsMatchingRefinement.OrderBy(p => p.Address.StateProvinceRegion);
                    break;
                case PropertiesSortBy.Name:
                    productsMatchingRefinement = productsMatchingRefinement.OrderBy(p => p.Name);
                    break;
            }

            return productsMatchingRefinement;
        }

        protected IEnumerable<T> FilterPropertiesBySaleDateRange(IEnumerable<T> productsMatchingRefinement, SearchPropertyRequest request)
        {
            var range = new DateRange(request.MinimumDateFilter, request.MaximumDateFilter);

            if (range.IsValid)
            {
                return productsMatchingRefinement.Where(x => range.Includes(x.SaleDate));
            }

            return productsMatchingRefinement;
        }

        protected IEnumerable<T> FilterPropertiesByMappingBoundaries(IEnumerable<T> productsMatchingRefinement, SearchPropertyRequest request)
        {
            IList<T> properties = productsMatchingRefinement.ToList();

            if (request.MappingBoundary == null ||
                request.MappingBoundary.SouthWest == null ||
                request.MappingBoundary.NorthEast == null ||
                request.MappingBoundary.SouthWest.Longitude == null ||
                request.MappingBoundary.NorthEast.Longitude == null ||
                request.MappingBoundary.SouthWest.Latitude == null ||
                request.MappingBoundary.NorthEast.Latitude == null)
            {
                return properties;
            }

            var lngFilteredProperties =
                    properties.Between(x => x.Longitude != null ? x.Longitude.Value : 0,
                                       request.MappingBoundary.SouthWest.Longitude.Value,
                                       request.MappingBoundary.NorthEast.Longitude.Value);

            return lngFilteredProperties.Between(x => x.Latitude != null ? x.Latitude.Value : 0,
                                                     request.MappingBoundary.SouthWest.Latitude.Value,
                                                     request.MappingBoundary.NorthEast.Latitude.Value);
        }
    }

    public static class EnumerableExtensions
    {
        /// <summary>
        /// Returns the values in a sequence whose resulting value of the specified 
        /// selector is between the lowest and highest values given in the parameters.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of elements in the sequence.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// Resulting value
        /// </typeparam>
        /// <param name="source">
        /// The IEnumerable object on which this method works.
        /// </param>
        /// <param name="selector">
        /// The selector predicate used to attain the value.
        /// </param>
        /// <param name="lowest">
        /// The lowest value of the selector that will appear in the new list.
        /// </param>
        /// <param name="highest">
        /// The hightest value of the selector that will appear in the new list.
        /// </param>
        /// <returns>
        /// An IEnumerable sequence of TSource whose selector values fall within the range 
        /// of <paramref name="lowest"/> and <paramref name="highest"/>.
        /// </returns>
        public static IEnumerable<TSource> Between<TSource, TResult>
        (
            this IEnumerable<TSource> source, Func<TSource, TResult> selector,
            TResult lowest, TResult highest
        )
            where TResult : IComparable<TResult>
        {
            return source.OrderBy(selector).
                SkipWhile(s => selector.Invoke(s).CompareTo(lowest) < 0).
                TakeWhile(s => selector.Invoke(s).CompareTo(highest) <= 0);
        }

        /// <summary>
        /// This is a simple test for the Between Linq extension method. We'll add a few
        /// values to a list and select only those that are between a certain range.
        /// When we're done, we should know the lowest and highest values contained
        /// in the resulting values set.
        /// </summary>
        //[TestMethod]
        //public void BetweenTest()
        //{
        //    var list = new List<double>();
        //    for (var i = 1; i <= 20; i++)
        //        list.Add(i);
        //    var fiveTo15 = list.Between(s => s, 5, 15);
        //    Assert.IsTrue(fiveTo15.Min() == 5);
        //    Assert.IsTrue(fiveTo15.Max() == 15);
        //}
    }
}