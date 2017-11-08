using FluentNHibernate;
using NH4CookbookHelpers.Mapping;
using NH4CookbookHelpers.Mapping.Model;
using NHibernate;
using NHibernate.Cfg;

namespace MappingRecipes.MappingWithFluent
{
    public class Recipe : BaseMappingRecipe
    {
        protected override void Configure(Configuration cfg)
        {
            cfg.AddMappingsFromAssembly(GetType().Assembly);
        }

        protected override void AddInitialData(ISession session)
        {
            session.Save(new Movie
            {
                Name = "Fluent mapping - the movie",
                Description = "Go with the flow.",
                UnitPrice = 300,
                Actors = {new ActorRole {Actor = "FNH", Role = "The mapper"}}
            });
        }
    }
}