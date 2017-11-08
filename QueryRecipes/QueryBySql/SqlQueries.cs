using System.Collections.Generic;
using NH4CookbookHelpers;
using NH4CookbookHelpers.Model;
using NHibernate;

namespace QueryRecipes.QueryBySql
{
    public class SqlQueries : IQueries
    {
        private readonly ISession _session;

        public SqlQueries(ISession session)
        {
            _session = session;
        }

        public IEnumerable<Movie> GetMoviesDirectedBy(string directorName)
        {
            var sql = @"select * from Product  
              where ProductType = 'Movie' and Director = :director";
            return _session.CreateSQLQuery(sql)
              .AddEntity(typeof(Movie))
              .SetString("director", directorName)
              .List<Movie>();
        }

        public IEnumerable<Movie> GetMoviesWith(string actorName)
        {
            var sql = @"select m.*
              from Product m
              inner join ActorRole as ar on ar.MovieId=m.Id
              where ar.Actor = :actorName";
            return _session.CreateSQLQuery(sql)
              .AddEntity(typeof(Movie))
              .SetString("actorName", actorName)
              .List<Movie>();
        }

        public Book GetBookByISBN(string isbn)
        {
            var sql = @"select b.* from Product b
              where b.ISBN = :isbn";
            return _session.CreateSQLQuery(sql)
              .AddEntity(typeof(Book))
              .SetString("isbn", isbn)
              .UniqueResult<Book>();
        }

        public IEnumerable<Product> GetProductsByPrice(
          decimal minPrice,
          decimal maxPrice)
        {
            var sql = @"select p.* from Product p
              where p.UnitPrice between :minPrice
              and :maxPrice
              order by p.UnitPrice asc";

            return _session.CreateSQLQuery(sql)
              .AddEntity(typeof(Product))
              .SetDecimal("minPrice", minPrice)
              .SetDecimal("maxPrice", maxPrice)
              .List<Product>();
        }

    }
}
