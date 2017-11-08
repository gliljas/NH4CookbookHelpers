using System.Collections.Generic;

namespace QueryRecipes.MultiQueries
{
    public struct PageOf<T>
    {
        public int PageCount;
        public int PageNumber;
        public IEnumerable<T> PageOfResults;
    }
}