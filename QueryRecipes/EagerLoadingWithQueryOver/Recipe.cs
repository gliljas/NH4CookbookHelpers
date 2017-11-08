using System.Linq;
using NH4CookbookHelpers;
using NH4CookbookHelpers.Model;
using NHibernate;
using NHibernate.Transform;

namespace QueryRecipes.EagerLoadingWithQueryOver
{
    public class Recipe : QueryRecipe
    {
        protected override void Run(ISession session)
        {
            var book = session.QueryOver<Book>()
                .Fetch(x => x.Publisher).Eager
                .SingleOrDefault();

            Show("Book:", book);

            var movies = session.QueryOver<Movie>()
                .Fetch(x => x.Actors).Eager
                .OrderBy(x => x.Name).Asc
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();

            Show("Movies:", movies);
        }
    }
}
