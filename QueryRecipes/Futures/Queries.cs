using System;
using System.Linq;
using NH4CookbookHelpers.Model;
using NHibernate;
using NHibernate.Linq;

namespace QueryRecipes.Futures
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

            var productCount = _session.Query<Product>().ToFutureValue(x=>x.Count());
            
            var products = GetPageQuery(skip, pageSize).Future<Product>();


            var pageCount = (int)Math.Ceiling(
            productCount.Value / (double)pageSize);

            return new PageOf<Product>()
            {
                PageCount = pageCount,
                PageOfResults = products,
                PageNumber = pageNumber
            };
        }
        
        private IQuery GetPageQuery(int skip, int take)
        {
            var hql = @"from Product p order by p.UnitPrice asc";
            return _session.CreateQuery(hql)
            .SetFirstResult(skip)
            .SetMaxResults(take);
        }
    }
}
