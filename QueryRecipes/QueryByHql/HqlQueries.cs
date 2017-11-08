using System.Collections.Generic;
using NH4CookbookHelpers;
using NH4CookbookHelpers.Model;
using NHibernate;

namespace QueryRecipes.QueryByHql
{
    public class HqlQueries : IQueries, IAggregateQueries
    {
        private readonly ISession _session;

        public HqlQueries(ISession session)
        {
            _session = session;
        }

        public IEnumerable<Movie> GetMoviesDirectedBy(string directorName)
        {
            var hql = @"from Movie m 
              where m.Director = :director";
            return _session.CreateQuery(hql)
              .SetString("director", directorName)
              .SetLockMode("m",LockMode.Upgrade)
              .List<Movie>();
        }

        public IEnumerable<Movie> GetMoviesWith(string actorName)
        {
            var hql = @"select m
              from Movie m
              inner join m.Actors as ar
              where ar.Actor = :actorName";
            return _session.CreateQuery(hql)
              .SetString("actorName", actorName)
              .List<Movie>();
        }

        public Book GetBookByISBN(string isbn)
        {
            var hql = @"from Book b
              where b.ISBN = :isbn";
            return _session.CreateQuery(hql)
              .SetString("isbn", isbn)
              .UniqueResult<Book>();
        }

        public IEnumerable<Product> GetProductsByPrice(
          decimal minPrice,
          decimal maxPrice)
        {
            var hql = @"from Product p
              where p.UnitPrice >= :minPrice
              and p.UnitPrice <= :maxPrice
              order by p.UnitPrice asc";

            return _session.CreateQuery(hql)
              .SetDecimal("minPrice", minPrice)
              .SetDecimal("maxPrice", maxPrice)
              .List<Product>();
        }

        public IEnumerable<NameAndPrice> GetMoviePriceList()
        {
            var hql = @"select new NameAndPrice(
              m.Name, m.UnitPrice)
              from Movie m";
            return _session.CreateQuery(hql)
              .List<NameAndPrice>();

        }

        public decimal GetAverageMoviePrice()
        {
            var hql = @"select Cast(avg(m.UnitPrice) 
              as Currency)
              from Movie m";
            return _session.CreateQuery(hql)
              .UniqueResult<decimal>();

        }

        public IEnumerable<NameAndPrice> GetAvgDirectorPrice()
        {
            var hql = @"select new NameAndPrice(
                m.Director, 
                Cast(avg(m.UnitPrice) as Currency)
              )
              from Movie m
              group by m.Director";
            return _session.CreateQuery(hql)
              .List<NameAndPrice>();

        }
    }
}
