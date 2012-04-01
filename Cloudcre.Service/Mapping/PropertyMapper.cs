using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Cloudcre.Model;
using Cloudcre.Service.Messages;
using Cloudcre.Service.ViewModels;

namespace Cloudcre.Service.Mapping
{
    public static class PropertyMapper
    {
        public static IEnumerable<TVm> ConvertToViewModels<T, TVm>(this IEnumerable<T> obj)
        {
            return Mapper.Map<IEnumerable<T>, IEnumerable<TVm>>(obj);
        }

        public static IEnumerable<T> ConvertToModels<T, TVm>(this IEnumerable<TVm> obj)
        {
            return Mapper.Map<IEnumerable<TVm>, IEnumerable<T>>(obj);
        }

        public static TVm ConvertToViewModel<T, TVm>(this T obj)
        {
            return Mapper.Map<T, TVm>(obj);
        }

        public static T ConvertToModel<T, TVm>(this TVm obj)
        {
            return Mapper.Map<TVm, T>(obj);
        }

        public static SearchPropertyResponse<TVm> CreatePropertiesSearchResultFrom<T, TVm>(this IEnumerable<T> propertiesMatchingQuery, SearchPropertyRequest request)
            where T : Property
            where TVm : PropertyViewModel
        {
            var propertySearchResultView = new SearchPropertyResponse<TVm>();
            propertySearchResultView.NumberOfTitlesFound = propertiesMatchingQuery.Count();
            propertySearchResultView.TotalNumberOfPages = NumberOfResultPagesGiven(request.NumberOfResultsPerPage,
                                                                                   propertySearchResultView.
                                                                                       NumberOfTitlesFound);
            propertySearchResultView.Properties = PropertyListBasedOnPageIndex<T, TVm>(request.Index,
                                                                               request.NumberOfResultsPerPage,
                                                                               propertiesMatchingQuery);
            propertySearchResultView.SqftMaxFilter = request.SqftMaxFilter;
            propertySearchResultView.SqftMinFilter = request.SqftMinFilter;
            //propertySearchResultView.SqftMax = propertiesMatchingQuery.Max(x => x.BuildingTotalSquareFoot);
            //propertySearchResultView.SqftMin = propertiesMatchingQuery.Min(x => x.BuildingTotalSquareFoot);

            propertySearchResultView.CurrentPage = request.Index;

            return propertySearchResultView;
        }

        private static int NumberOfResultPagesGiven(int numberOfResultsPerPage, int numberOfTitlesFound)
        {
            if (numberOfTitlesFound < numberOfResultsPerPage)
            {
                return 1;
            }

            return (int) Math.Ceiling((decimal) numberOfTitlesFound/numberOfResultsPerPage);
        }

        private static IEnumerable<TVm> PropertyListBasedOnPageIndex<T, TVm>(int pageIndex, int numberOfResultsPerPage, IEnumerable<T> productsFound)
        {
            if (pageIndex > 1)
            {
                int numToSkip = (pageIndex - 1)*numberOfResultsPerPage;
                return productsFound.Skip(numToSkip).Take(numberOfResultsPerPage).ConvertToViewModels<T, TVm>();
            }

            return productsFound.Take(numberOfResultsPerPage).ConvertToViewModels<T, TVm>();
        }
    }
}