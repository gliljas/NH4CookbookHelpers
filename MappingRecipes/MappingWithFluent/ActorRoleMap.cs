using FluentNHibernate.Mapping;
using NH4CookbookHelpers.Mapping.Model;

namespace MappingRecipes.MappingWithFluent
{
    public class ActorRoleMap : ClassMap<ActorRole>
    {
        public ActorRoleMap()
        {
            Id(ar => ar.Id).GeneratedBy.GuidComb();
            Map(ar => ar.Actor).Not.Nullable();
            Map(ar => ar.Role).Not.Nullable();
        }
    }
}