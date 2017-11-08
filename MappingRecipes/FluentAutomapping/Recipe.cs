using FluentNHibernate;
using FluentNHibernate.Automapping;
using NH4CookbookHelpers.Mapping;
using NH4CookbookHelpers.Mapping.Model;
using NHibernate;
using NHibernate.Cfg;

namespace MappingRecipes.FluentAutomapping
{
    public class Recipe : BaseMappingRecipe
    {
        protected override void Configure(Configuration cfg)
        {
            var persistenceModel = AutoMap.AssemblyOf<Product>(new AutomappingConfiguration())
                .Conventions.AddFromAssemblyOf<Conventions>();

            cfg.AddAutoMappings(persistenceModel);
        }

        protected override void AddInitialData(ISession session)
        {
            session.Save(new Movie
            {
                Name = "Automapping - the movie",
                Description = "A conventional movie.",
                UnitPrice = 300,
                Actors = {new ActorRole {Actor = "FNH", Role = "The mapper"}}
            });
        }
    }
}