using NH4CookbookHelpers;
using NHibernate;

namespace QueryRecipes.QueryByQueryOver
{
    public class Recipe : QueryRecipe
    {
        protected override void Run(ISession session)
        {
            var queries = new QueryOverQueries(session);
            ShowQueryResults(queries);
        }
    }
}
