using System;
using System.Collections.Generic;
using System.Linq;
using NH4CookbookHelpers.Model;
using NHibernate;

namespace QueryRecipes.MultiQueries
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

            var multiQuery = _session.CreateMultiQuery()
            .Add<long>("count", countQuery)
            .Add<Product>("page", resultQuery);

            var productCount = ((IList<long>)multiQuery
            .GetResult("count")).Single();

            var products = (IList<Product>)multiQuery
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

        private IQuery GetCountQuery()
        {
            var hql = @"select count(p.Id) from Product p";
            return _session.CreateQuery(hql);
        }

        private IQuery GetPageQuery(int skip, int take)
        {
            var hql = @"select {p.*} from Product p order by p.UnitPrice asc";
            return _session.CreateSQLQuery(hql)
                .AddEntity("p",typeof(Product))
            .SetFirstResult(skip)
            .SetMaxResults(take);
        }

    }
}
