using NH4CookbookHelpers;
using NH4CookbookHelpers.Model;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;

namespace QueryRecipes.QueryByHql
{
    public class Recipe : QueryRecipe
    {
        protected override void Configure(Configuration nhConfig)
        {
var modelMapper = new ModelMapper();
modelMapper.Import<NameAndPrice>();
var mapping = modelMapper.CompileMappingForAllExplicitlyAddedEntities();
nhConfig.AddMapping(mapping);
        }

        protected override void Run(ISession session)
        {
            var queries = new HqlQueries(session);
            ShowQueryResults(queries);
            ShowAggregateQueryResults(queries);
        }
    }
}
