using System.Collections.Generic;
using NH4CookbookHelpers;
using NH4CookbookHelpers.Model;
using NHibernate;

namespace QueryRecipes.QueryByQueryOver
{
    public class QueryOverQueries : IQueries
    {
        private readonly ISession _session;

        public QueryOverQueries(ISession session)
        {
            _session = session;
        }

        public IEnumerable<Movie> GetMoviesDirectedBy(string directorName)
        {
            return _session.QueryOver<Movie>()
                .Where(x => x.Director == directorName)
                .List();
        }

        public IEnumerable<Movie> GetMoviesWith(string actorName)
        {
            return _session.QueryOver<Movie>()
                .Inner.JoinQueryOver<ActorRole>(m => m.Actors)
                .Where(a => a.Actor == actorName)
                .List();
        }

        public Book GetBookByISBN(string isbn)
        {
            return _session.QueryOver<Book>()
                   .Where(b => b.ISBN == isbn)
                   .SingleOrDefault();
        }

        public IEnumerable<Product> GetProductsByPrice(decimal minPrice, decimal maxPrice)
        {
            return _session.QueryOver<Product>()
                .Where(x => x.UnitPrice >= minPrice && x.UnitPrice <= maxPrice)
                .OrderBy(p => p.UnitPrice).Asc
                .List();
        }
    }
}
