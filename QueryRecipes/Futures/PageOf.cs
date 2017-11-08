using System.Collections.Generic;

namespace QueryRecipes.Futures
{
    public struct PageOf<T>
    {
        public int PageCount;
        public int PageNumber;
        public IEnumerable<T> PageOfResults;
    }
}