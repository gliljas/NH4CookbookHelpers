using NH4CookbookHelpers;
using NH4CookbookHelpers.Model;
using NHibernate;
using NHibernate.Transform;

namespace QueryRecipes.EagerLoadingWithHql
{
    public class Recipe : QueryRecipe
    {
        protected override void Run(ISession session)
        {
            var book = session.CreateQuery(@"
                  from Book b
                  left join fetch b.Publisher")
                 .UniqueResult<Book>();

            Show("Book:", book);

            var movies = session.CreateQuery(@"
                  from Movie m
                  left join fetch m.Actors
               ")
              .SetResultTransformer(Transformers.DistinctRootEntity)
              .List<Movie>();

            Show("Movies:", movies);
        }
    }
}
