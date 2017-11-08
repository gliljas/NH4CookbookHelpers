using System.Linq;
using NH4CookbookHelpers;
using NH4CookbookHelpers.Model;
using NHibernate;
using NHibernate.Linq;

namespace QueryRecipes.AdvancedEagerLoading
{
    public class Recipe : QueryRecipe
    {
        protected override void AddData(ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    for (var i = 1; i <= 20; i++)
                    {
                        var movie = new Movie
                        {
                            Name = "Movie" + i,
                            UnitPrice = i
                        };
                        movie.AddActor("Actor" + i, "Role" + i);
                        movie.AddActor("Second Actor" + 1, "Second Role" + i);
                        session.Save(movie);
                    }
                    tx.Commit();
                }
            }
        }

        protected override void Run(ISession session)
        {
            //var baseCrit = DetachedCriteria.For<Movie>()
            //    .Add(Restrictions.Like("Name", "Movie", MatchMode.Start))
            //    .AddOrder(new Order("Name", true))
            //    .SetProjection(Property.ForName("Id"))
            //    .SetFirstResult(5)
            //    .SetMaxResults(5);

            //var movies = session.CreateCriteria<Movie>()
            //    .Add(Subqueries.PropertyIn("Id", baseCrit))
            //    .AddOrder(new Order("Name", true))
            //    .SetFetchMode("Actors", FetchMode.Join)
            //    .List<Movie>();

            //var baseQuery = QueryOver.Of<Movie>()
            //    .Where(
            //        Restrictions.On<Movie>(x => x.Name)
            //        .IsLike("Movie", MatchMode.Start)
            //    )
            //    .OrderBy(x => x.Name).Asc
            //    .Select(x=>x.Id)
            //    .Skip(5)
            //    .Take(5);


            //var movies = session.QueryOver<Movie>()
            //    .WithSubquery
            //    .WhereProperty(m => m.Id)
            //    .In(baseQuery)
            //    .Fetch(x => x.Actors).Eager
            //    .List();

            var baseQuery = session.Query<Movie>()
                .Where(x => x.Name.StartsWith("Movie"))
                .OrderBy(x => x.Name)
                .Skip(5)
                .Take(5);

            var movies = session.Query<Movie>()
                .Where(x => baseQuery.Select(b=>b.Id).Select(b => b).Any(b=>b==x.Id))
                .OrderBy(x => x.Name)
                .FetchMany(x => x.Actors)
                .ToList();

            Show("A page of movies", movies);

            //var allProducts = session.Query<Product>()
            //    .OrderBy(x => x.UnitPrice)
            //    .ToFuture();

            //session.Query<Movie>()
            //    .FetchMany(x => x.Actors)
            //    .ToFuture();

            //session.Query<Book>()
            //    .Fetch(x => x.Publisher)
            //    .ToFuture();

            //Show("All products", allProducts);
        }
    }
}
