using NH4CookbookHelpers.Model;
using NHibernate;

namespace QueryRecipes.NamedQueries
{
    public class NamedQueries
    {
        private readonly ISession _session;

        public NamedQueries(ISession session)
        {
            _session = session;
        }
        
        public Book GetBookByISBN(string isbn)
        {
            return _session.GetNamedQuery("GetBookByISBN")
                   .SetString("isbn", isbn)
                   .UniqueResult<Book>();
        }
    }
}
