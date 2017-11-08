using NH4CookbookHelpers;
using NHibernate;

namespace QueryRecipes.QueryOverProjections
{
    public class Recipe : QueryRecipe
    {
        protected override void Run(ISession session)
        {
            var queries = new QueryOverAggregateQueries(session);
            ShowAggregateQueryResults(queries);
        }
    }
}
