using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NH4CookbookHelpers.Model
{
    public class ActorRoleMapping : ClassMapping<ActorRole>
    {
        public ActorRoleMapping()
        {
            Id(x => x.Id, x => x.Generator(new NativeGeneratorDef()));
            Property(x => x.Actor);
            Property(x => x.Role);
            ManyToOne(x=>x.Movie,m=>m.Column("MovieId"));
     

        }
    }
}