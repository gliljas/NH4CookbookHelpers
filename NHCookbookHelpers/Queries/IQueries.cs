using System.Collections.Generic;
using NH4CookbookHelpers.Model;

namespace NH4CookbookHelpers
{
    public interface IQueries
    {
        IEnumerable<Movie> GetMoviesDirectedBy(string directorName);
        IEnumerable<Movie> GetMoviesWith(string actorName);
        Book GetBookByISBN(string isbn);
        IEnumerable<Product> GetProductsByPrice(decimal minPrice, decimal maxPrice);
    }
}
