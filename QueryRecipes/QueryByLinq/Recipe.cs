using NH4CookbookHelpers;
using NHibernate;

namespace QueryRecipes.QueryByLinq
{
    public class Recipe : QueryRecipe
    {
        protected override void Run(ISession session)
        {
            var queries = new LinqQueries(session);
            ShowQueryResults(queries);
        }
    }
}
