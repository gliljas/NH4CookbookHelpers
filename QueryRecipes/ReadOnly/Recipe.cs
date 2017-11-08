using NH4CookbookHelpers;
using NH4CookbookHelpers.Model;
using NHibernate;

namespace QueryRecipes.ReadOnly
{
    public class Recipe : QueryRecipe
    {
        private bool _readOnly=true;

        protected override void Run(ISessionFactory sessionFactory)
        {
            RunWithReadOnlySession(sessionFactory);
            RunWithQuery(sessionFactory);
            RunWithSetReadOnly(sessionFactory);
        }

        private void RunWithReadOnlySession(ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            {
                session.DefaultReadOnly = _readOnly;
                using (var tx = session.BeginTransaction())
                {
                    var movie = session.Get<Movie>(1);
                    movie.Director = "Updated in session";
                    tx.Commit();
                }
            }
        }
        
        private void RunWithQuery(ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    var query = session.QueryOver<Movie>()
                        .Where(x => x.Id == 1);
                   
                    if (_readOnly)
                    {
                        query.ReadOnly();
                    }
                    var movie=query.SingleOrDefault();

                    movie.Director = "Updated in query";
                    tx.Commit();
                }
            }
        }

        private void RunWithSetReadOnly(ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    var movie = session.Get<Movie>(1);
                    session.SetReadOnly(movie, true);
                    movie.Director = "Updated with SetReadOnly";
                    tx.Commit();
                }
            }
        }
    }
}
