using System.Linq;
using NH4CookbookHelpers;
using NH4CookbookHelpers.Model;
using NHibernate;
using NHibernate.Linq;

namespace QueryRecipes.EagerLoadingWithLinq
{
    public class Recipe : QueryRecipe
    {
        protected override void Run(ISession session)
        {
            var book = session.Query<Book>()
                .Fetch(x => x.Publisher)
                .FirstOrDefault();

            Show("Book:",
                book);

            var movies = session.Query<Movie>()
                .FetchMany(x => x.Actors)
                .OrderBy(x=>x.Name)
                .ToList();
            Show("Movies:",
                movies);
        }
    }
}
