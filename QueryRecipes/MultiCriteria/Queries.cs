using System;
using System.Collections.Generic;
using System.Linq;
using NH4CookbookHelpers.Model;
using NHibernate;

namespace QueryRecipes.MultiCriteria
{
    public class Queries
    {
        private readonly ISession _session;

        public Queries(ISession session)
        {
            _session = session;
        }

        public PageOf<Product> GetPageOfProducts(
int pageNumber,
int pageSize)
        {
            var skip = (pageNumber - 1) * pageSize;

            var countQuery = GetCountQuery();
            var resultQuery = GetPageQuery(skip, pageSize);

            var multiCrit = _session.CreateMultiCriteria()
            .Add<int>("count", countQuery)
            .Add<Product>("page", resultQuery);

            var productCount = ((IList<int>)multiCrit
            .GetResult("count")).Single();

            var products = (IList<Product>)multiCrit
            .GetResult("page");

            var pageCount = (int)Math.Ceiling(
            productCount / (double)pageSize);

            return new PageOf<Product>()
            {
                PageCount = pageCount,
                PageOfResults = products,
                PageNumber = pageNumber
            };
        }

        private ICriteria GetCountQuery()
        {
            return _session.QueryOver<Product>()
            .SelectList(list => list
            .SelectCount(m => m.Id))
            .UnderlyingCriteria;
        }

        private ICriteria GetPageQuery(int skip, int take)
        {
            return _session.QueryOver<Product>()
            .OrderBy(m => m.UnitPrice).Asc
            .Skip(skip)
            .Take(take)
            .UnderlyingCriteria;
        }
    }
}
