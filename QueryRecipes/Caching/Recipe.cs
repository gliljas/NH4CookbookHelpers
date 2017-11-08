using NH4CookbookHelpers;
using NH4CookbookHelpers.Model;
using NHibernate;
using NHibernate.Cfg;

namespace QueryRecipes.Caching
{
    public class Recipe : QueryRecipe
    {
        protected override void Configure(Configuration nhConfig)
        {
            nhConfig.Configure("Caching/hibernate.cfg.xml");
        }


        protected override void Run(ISessionFactory sessionFactory)
        {

            ShowMoviesBy(sessionFactory, "Steven Spielberg");
            ShowMoviesBy(sessionFactory, "Steven Spielberg");
            UpdateMoviesBy(sessionFactory, "Rob Reiner");
            ShowMoviesBy(sessionFactory, "Steven Spielberg");

        }

        private void UpdateMoviesBy(ISessionFactory sessionFactory, string director)
        {
            LogMessage("Update movies");
            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    session.CreateQuery(@"update Movie 
                                        set Description='Good' 
                                        where Director=:director")
                        .SetString("director", director)
                        .ExecuteUpdate();
                    tx.Commit();
                }
            }
        }

        private
            void ShowMoviesBy(ISessionFactory sessionFactory, string director)
        {
            LogMessage("Query movies");
            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    var movies = session.QueryOver<Movie>()
                        .Where(x => x.Director == director)
                        .Cacheable()
                        .List();
                    Show("Movies found:", movies);
                    tx.Commit();
                }
            }
        }
    }
}
