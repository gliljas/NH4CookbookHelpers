using System.Collections.Generic;
using NH4CookbookHelpers;
using NH4CookbookHelpers.Model;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;

namespace QueryRecipes.QueryByCriteria
{
    public class CriteriaQueries : IQueries
    {
        private readonly ISession _session;

        public CriteriaQueries(ISession session)
        {
            _session = session;
        }

        public IEnumerable<Movie> GetMoviesDirectedBy(string directorName)
        {
            return _session.CreateCriteria<Movie>()
              .Add(Restrictions.Eq("Director", directorName))
              .List<Movie>();
        }


        public IEnumerable<Movie> GetMoviesWith(string actorName)
        {
            return _session.CreateCriteria<Movie>()
              .CreateCriteria("Actors", JoinType.InnerJoin)
              .Add(Restrictions.Eq("Actor", actorName))
              .List<Movie>();
        }


        public Book GetBookByISBN(string isbn)
        {
            return _session.CreateCriteria<Book>()
              .Add(Restrictions.Eq("ISBN", isbn))
              .UniqueResult<Book>();
        }


        public IEnumerable<Product> GetProductsByPrice(decimal minPrice, decimal maxPrice)
        {
            return _session.CreateCriteria<Product>()
              .Add(Restrictions.And(
                Restrictions.Ge("UnitPrice", minPrice),
                Restrictions.Le("UnitPrice", maxPrice)
                     ))
              .AddOrder(Order.Asc("UnitPrice"))
              .List<Product>();
        }
    }
}
