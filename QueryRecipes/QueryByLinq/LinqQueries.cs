using System.Collections.Generic;
using System.Linq;
using NH4CookbookHelpers;
using NH4CookbookHelpers.Model;
using NHibernate;
using NHibernate.Linq;

namespace QueryRecipes.QueryByLinq
{
    public class LinqQueries : IQueries
    {
        private readonly ISession _session;

        public LinqQueries(ISession session)
        {
            _session = session;
        }

        public IEnumerable<Movie> GetMoviesDirectedBy(string directorName)
        {
            return _session.Query<Movie>()
                .Where(x => x.Director == directorName)
                .ToList();
        }

        public IEnumerable<Movie> GetMoviesWith(string actorName)
        {
            return _session.Query<Movie>()
                .Where(x => x.Actors.Any(ar => ar.Actor == actorName))
                .ToList();
        }

        public Book GetBookByISBN(string isbn)
        {
            return _session.Query<Book>()
                .FirstOrDefault(x => x.ISBN == isbn);
        }

        public IEnumerable<Product> GetProductsByPrice(decimal minPrice, decimal maxPrice)
        {
            return
            _session.Query<Product>()
                .Where(x =>
                    x.UnitPrice >= minPrice &&
                    x.UnitPrice <= maxPrice
                )
                .OrderBy(x => x.UnitPrice)
                .ToList();
        }
    }
}
