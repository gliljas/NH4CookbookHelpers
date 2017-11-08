using NH4CookbookHelpers;
using NH4CookbookHelpers.Model;
using NHibernate;
using NHibernate.Caches.SysCache;
using NHibernate.Cfg;

namespace QueryRecipes.CachingWithCode
{
    public class Recipe : QueryRecipe
    {
        protected override void Configure(Configuration nhConfig)
        {
            nhConfig
                .Cache(x =>
                {
                    x.Provider<SysCacheProvider>();
                    x.UseQueryCache = true;
                }).EntityCache<Product>(c =>
                {
                    c.Strategy = EntityCacheUsage.ReadWrite;
                    c.RegionName = "hourly";
                })
                .EntityCache<ActorRole>(c =>
                {
                    c.Strategy = EntityCacheUsage.ReadWrite;
                    c.RegionName = "hourly";
                })
                .EntityCache<Movie>(c => c.Collection(
                movie => movie.Actors,
                coll =>
                {
                    coll.Strategy = EntityCacheUsage.ReadWrite;
                    coll.RegionName = "hourly";
                }));
        }

        protected override void Run(ISessionFactory sessionFactory)
        {
            ShowMoviesBy(sessionFactory, "Steven Spielberg");
            ShowMoviesBy(sessionFactory, "Steven Spielberg");
        }

        private void ShowMoviesBy(ISessionFactory sessionFactory, string director)
        {
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
