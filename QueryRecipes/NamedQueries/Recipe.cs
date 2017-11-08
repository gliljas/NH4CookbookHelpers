using NH4CookbookHelpers;
using NHibernate;
using NHibernate.Cfg;

namespace QueryRecipes.NamedQueries
{
    public class Recipe : QueryRecipe
    {
        protected override void Configure(Configuration nhConfig)
        {
            nhConfig.AddResource("QueryRecipes.NamedQueries.Queries.hbm.xml", GetType().Assembly);
        }

        protected override void Run(ISession session)
        {
            var queries = new NamedQueries(session);
            Show("This book:",
                queries.GetBookByISBN(
               "Steven Spielberg"));
        }
    }
}
