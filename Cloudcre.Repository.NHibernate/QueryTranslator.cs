using System;
using System.Text;
using Cloudcre.Model.Core.Querying;

namespace Cloudcre.Repository.NHibernate
{
    public static class QueryTranslator
    {
        public static string[] TranslateIntoLuceneSearchQuery(this SeachQueryParameters query)
        {
            var lquery = new StringBuilder();

            if ((query & SeachQueryParameters.AddressLine1) == SeachQueryParameters.AddressLine1)
            {
                lquery.Append("Address.AddressLine1,");
            }

            if ((query & SeachQueryParameters.City) == SeachQueryParameters.City)
            {
                lquery.Append("Address.City,");
            }

            if ((query & SeachQueryParameters.County) == SeachQueryParameters.County)
            {
                lquery.Append("Address.County,");
            }

            if ((query & SeachQueryParameters.Name) == SeachQueryParameters.Name)
            {
                lquery.Append("Name,");
            }

            if ((query & SeachQueryParameters.ParcelId) == SeachQueryParameters.ParcelId)
            {
                lquery.Append("Site.ParcelId,");
            }

            if ((query & SeachQueryParameters.Zip) == SeachQueryParameters.Zip)
            {
                lquery.Append("Address.Zip,");
            }

            return lquery.ToString().Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}