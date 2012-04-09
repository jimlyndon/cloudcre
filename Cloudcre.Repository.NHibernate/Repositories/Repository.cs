using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloudcre.Model.Core;
using Cloudcre.Model.Core.Querying;
using Cloudcre.Model.Core.UnitOfWork;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.QueryParsers;
using NHibernate;
using NHibernate.Search;

namespace Cloudcre.Repository.NHibernate.Repositories
{
    public abstract class Repository<T, TEntityKey> where T : class, IAggregateRoot
    {
        private readonly IUnitOfWork _uow;
        private readonly IFullTextSession _session;

        protected Repository(IUnitOfWork uow, IFullTextSession session)
        {
            _uow = uow;
            _session = session;
        }

        public void Add(T entity)
        {
            _uow.RegisterNew(entity, null);
        }

        public void Remove(T entity)
        {
            _uow.RegisterRemoved(entity, null);
        }

        public void Save(T entity)
        {
            _uow.RegisterAmended(entity, null);
        }

        private T One(IEnumerable<T> items, bool throwIfNone)
        {
            List<T> itemsList = items.ToList();
            if (throwIfNone && itemsList.Count == 0)
            {
                throw new Exception(string.Format("Expected at least one '{0}' in the query results", typeof(T).Name));
            }
            return itemsList.Count > 0
                       ? itemsList[0]
                       : default(T);
        }

        public T FindFirst(Specification<T> specification)
        {
            return One(FindAll(specification), true);
        }

        public T FindFirstOrDefault(Specification<T> specification)
        {
            return One(FindAll(specification), false);
        }

        public T FindBy(TEntityKey id)
        {
            return _session.Get<T>(id);
        }

        public IEnumerable<T> FindAll()
        {
            ICriteria criteriaQuery = _session.CreateCriteria(typeof(T));
            return criteriaQuery.List<T>();
        }

        public IEnumerable<T> FindAll(int index, int count)
        {
            ICriteria criteriaQuery = _session.CreateCriteria(typeof(T));
            return criteriaQuery.SetFetchSize(count).SetFirstResult(index).List<T>();
        }

        public IEnumerable<T> FindAll(Specification<T> specification)
        {
            var query = _session.QueryOver<T>().Where(specification.IsSatisfied());
            return query.List();
        }

        public IEnumerable<T> FindAll(Specification<T> specification, int index, int count)
        {
            int numberOfResultsToSkip = (index - 1) * count;
            var query = _session.QueryOver<T>().Where(specification.IsSatisfied()).Skip(numberOfResultsToSkip).Take(count);
            return query.List();
        }

        public IEnumerable<T> FindBy(SeachQuery[] queries)
        {
            string result = BuildQuery(queries);
            return _session.CreateFullTextQuery<T>(result).Enumerable<T>();
        }

        public IEnumerable<T> FindBy(SeachQuery[] queries, int index, int count)
        {
            return _session.CreateFullTextQuery<T>(BuildQuery(queries)).SetFetchSize(count).SetFirstResult(index).Enumerable<T>();
        }

        public IEnumerable<T> FindBy(Specification<T> specification, SeachQuery[] queries)
        {
            return _session.CreateFullTextQuery<T>(BuildQuery(queries)).Enumerable<T>().AsQueryable().Where(specification.IsSatisfied()).AsEnumerable();
        }

        public IEnumerable<T> FindBy(Specification<T> specification, SeachQuery[] queries, int index, int count)
        {
            return _session.CreateFullTextQuery<T>(BuildQuery(queries)).SetFetchSize(count).SetFirstResult(index).Enumerable<T>().AsQueryable().Where(specification.IsSatisfied()).AsEnumerable();
        }

        private string BuildQuery(IEnumerable<SeachQuery> queryObjects)
        {
            var andQueries = new List<string>();
            var orQueries = new List<string>();
            foreach(var queryObject in queryObjects)
            {
                // build parser specifying fields to search
                string[] fields = queryObject.SearchFields.TranslateIntoLuceneSearchQuery();
                var parser = new MultiFieldQueryParser(fields, new StandardAnalyzer());
                parser.SetDefaultOperator(QueryParser.Operator.AND);

                // build wild card query for each term
                if (queryObject.TokenizeQuery)
                {
                    string[] searchQueryTokenized = queryObject.Query.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (searchQueryTokenized.Length > 0)
                    {
                        string wildCardValue = (queryObject.WildCard) ? "*" : string.Empty;
                        string luceneQuery = searchQueryTokenized.Aggregate((a, b) => a + wildCardValue + " " + b) + wildCardValue;
                        andQueries.Add(parser.Parse(luceneQuery).ToString());
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(queryObject.Query))
                    {
                        var builder = new StringBuilder();

                        string wildCardValue = (queryObject.WildCard) ? "*" : string.Empty;

                        foreach (var field in fields)
                        {
                            builder.Append(field + ":" + queryObject.Query + wildCardValue + " ");
                        }

                        orQueries.Add(builder.ToString());
                    }
                }
            };

            string orString = (orQueries.Count == 0) ? string.Empty : orQueries.Aggregate((a, b) => "(" + a + ") OR (" + b + ")");
            string andString = (andQueries.Count == 0) ? string.Empty : andQueries.Aggregate((a, b) => "(" + a + ") AND (" + b + ")");

            if (orString.Length > 0 && andString.Length > 0)
                return "(" + orString + ") AND (" + andString + ")";
            if (orString.Length > 0)
                return orString;
            return andString;
        }

        //public IEnumerable<T> FindBy(Query query)
        //{
        //    ICriteria nhQuery = query.TranslateIntoNHQuery<T>();
        //    return nhQuery.List<T>();
        //}

        //public IEnumerable<T> FindBy(Query query, int index, int count)
        //{
        //    ICriteria nhQuery = query.TranslateIntoNHQuery<T>();
        //    return nhQuery.SetFetchSize(count).SetFirstResult(index).List<T>();
        //}
    }
}