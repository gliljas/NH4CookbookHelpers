using NH4CookbookHelpers.Mapping.Model;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MappingRecipes.MappingByCode
{
    public class MovieMapping : SubclassMapping<Movie>
    {
        public MovieMapping()
        {
            DiscriminatorValue("Movie");
            Property(x => x.Director);
            List(x => x.Actors, x =>
                {
                    x.Key(k => k.Column("MovieId"));
                    x.Index(i => i.Column("ActorIndex"));
                    x.Cascade(Cascade.All | Cascade.DeleteOrphans);
                }
                , x => x.OneToMany()
            );
        }
    }
}