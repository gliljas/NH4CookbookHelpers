using NH4CookbookHelpers.Mapping.Model;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MappingRecipes.MappingByCode
{
    public class ActorRoleMapping : ClassMapping<ActorRole>
    {
        public ActorRoleMapping()
        {
            Id(x => x.Id, x => x.Generator(new GuidCombGeneratorDef()));
            Property(x => x.Actor);
            Property(x => x.Role);
        }
    }
}