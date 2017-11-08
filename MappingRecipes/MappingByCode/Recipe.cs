using System.Collections.Generic;
using NH4CookbookHelpers.Mapping;
using NH4CookbookHelpers.Mapping.Model;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;

namespace MappingRecipes.MappingByCode
{
    public class Recipe : BaseMappingRecipe
    {
        protected override void Configure(Configuration cfg)
        {
            var mapper = new ModelMapper();

            mapper.AddMapping<ProductMapping>();
            mapper.AddMapping<MovieMapping>();
            mapper.AddMapping<BookMapping>();
            mapper.AddMapping<ActorRoleMapping>();

            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            cfg.AddMapping(mapping);
        }


        protected override void AddInitialData(ISession session)
        {
            session.Save(new Movie
            {
                Name = "Mapping by code - the movie",
                Description = "An interesting documentary",
                UnitPrice = 300,
                Actors = new List<ActorRole> {new ActorRole {Actor = "You", Role = "The mapper"}}
            });
        }
    }
}