using System.Collections.Generic;
using NH4CookbookHelpers.Model;

namespace ActionFilterExample
{
public static class DataAccessLayer
{
    public static IEnumerable<Book> GetBooks()
    {
        var session = MvcApplication.SessionFactory
            .GetCurrentSession();
        using (var tx = session.BeginTransaction())
        {
            var books = session.QueryOver<Book>()
                .List();
            tx.Commit();
            return books;
        }
    }
}
}

