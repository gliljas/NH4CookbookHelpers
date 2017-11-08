using FluentNHibernate.Mapping;
using NH4CookbookHelpers.Mapping.Model;

namespace MappingRecipes.MappingWithFluent
{
    public class MovieMap : SubclassMap<Movie>
    {
        public MovieMap()
        {
            DiscriminatorValue("Movie");
            Map(m => m.Director);
            HasMany(m => m.Actors)
                .KeyColumn("MovieId")
                .AsList(l => l.Column("ActorIndex"))
                .Cascade.AllDeleteOrphan();
        }
    }
}