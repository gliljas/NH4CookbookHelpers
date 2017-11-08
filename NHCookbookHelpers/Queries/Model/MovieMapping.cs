using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NH4CookbookHelpers.Model
{
    public class MovieMapping : SubclassMapping<Movie>
    {
        public MovieMapping()
        {
            DiscriminatorValue("Movie");
            Property(x => x.Director);
            Set(x => x.Actors, x =>
                {
                    x.Inverse(true);
                    x.Key(k => k.Column("MovieId"));
                    x.Cascade(Cascade.All);
                    x.Access(Accessor.Field);
                }
            , x => x.OneToMany());
        }
    }
}
