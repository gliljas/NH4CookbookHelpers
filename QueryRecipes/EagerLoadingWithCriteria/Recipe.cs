using NH4CookbookHelpers;
using NH4CookbookHelpers.Model;
using NHibernate;
using NHibernate.Transform;

namespace QueryRecipes.EagerLoadingWithCriteria
{
    public class Recipe : QueryRecipe
    {
        protected override void Run(ISession session)
        {
            //var book = session.CreateCriteria<Book>()
            //    .SetFetchMode("Publisher", FetchMode.Join)
            //    .UniqueResult<Book>();

            //Show("Book:", book);

            var movies = session.CreateCriteria<Movie>()
                //.CreateAlias("Actors","a")
                .SetFetchMode("Actors",FetchMode.Join)
                .SetFetchMode("Actors.BookAboutTheRole", FetchMode.Join)
      //          .SetResultTransformer(Transformers.DistinctRootEntity)
                .List<Movie>();

            Show("Movies:", movies);
        }
    }
}
