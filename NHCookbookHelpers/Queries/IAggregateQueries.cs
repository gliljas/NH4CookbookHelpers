using System.Collections.Generic;
using NH4CookbookHelpers.Model;

namespace NH4CookbookHelpers
{
    public interface IAggregateQueries
    {
        IEnumerable<NameAndPrice> GetMoviePriceList();
        decimal GetAverageMoviePrice();
        IEnumerable<NameAndPrice> GetAvgDirectorPrice();
    }
}