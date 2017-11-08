using System.Collections.Generic;
using NH4CookbookHelpers.Model;

namespace NH4CookbookHelpers
{
    public interface IEagerLoadingQueries
    {
        IEnumerable<Product> GetAllProducts();
        
    }
}