using NH4CookbookHelpers;
using NHibernate;

namespace QueryRecipes.QueryBySql
{
    public class Recipe : QueryRecipe
    {
        protected override void Run(ISession session)
        {
            var queries=new SqlQueries(session);
            ShowQueryResults(queries);
        }
    }
}
