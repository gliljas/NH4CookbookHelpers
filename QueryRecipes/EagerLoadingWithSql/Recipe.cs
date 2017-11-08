using NH4CookbookHelpers;
using NH4CookbookHelpers.Model;
using NHibernate;
using NHibernate.Transform;

namespace QueryRecipes.EagerLoadingWithSql
{
    public class Recipe : QueryRecipe
    {
        protected override void Run(ISession session)
        {
            var book = session.CreateSQLQuery(@"
                  select {b.*}, {p.*} from Product b
                  left join Publisher p ON b.PublisherId=p.Id
                  where b.ProductType = 'Book'")
                .AddEntity("b", typeof(Book))
                .AddJoin("p", "b.Publisher")
                .UniqueResult<Book>();

            Show("Book:", book);

            var movies = session.CreateSQLQuery(@"
                  select {m.*}, {ar.*} from Product m
                  left join ActorRole ar ON ar.MovieId=m.Id
                  where m.ProductType = 'Movie' 
               ")
              .AddEntity("m", typeof(Movie))
              .AddJoin("ar", "m.Actors")
              .AddEntity("m", typeof(Movie))
              .SetResultTransformer(Transformers.DistinctRootEntity)
              .List<Movie>();

            Show("Movies:", movies);
        }
    }
}
