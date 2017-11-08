using System;
using System.Linq;
using NH4CookbookHelpers;
using NH4CookbookHelpers.Model;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;

namespace QueryRecipes.Stateless
{
    public class Recipe : QueryRecipe
    {
        protected override void Configure(Configuration nhConfig)
        {
            nhConfig.SetProperty(NHibernate.Cfg.Environment.BatchSize, "100");
        }


        protected override void AddData(ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenStatelessSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    for (int i = 0; i < 1000; i++)
                        session.Insert(new Movie()
                        {
                            Name = "Movie " + i,
                            Description = "A great movie!",
                            UnitPrice = 14.95M,
                            Director = "Johnny Smith"
                        });
                    tx.Commit();
                }
            }
        }

        protected override void Run(ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenStatelessSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    var movies = session.Query<Movie>().ToList();
                    foreach (var movie in movies)
                    {
                        UpdateMoviePrice(movie);
                        session.Update(movie);
                    }
                    tx.Commit();
                }
            }
        }

        static Random rnd = new Random();

        static void UpdateMoviePrice(Movie movie)
        {
            // Random price between $9.95 and $24.95
            movie.UnitPrice = (decimal)rnd.Next(10, 26) - 0.05M;
        }

    }
}
