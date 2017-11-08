using NH4CookbookHelpers.Mapping.Model;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MappingRecipes.MappingByCode
{
    public class BookMapping : SubclassMapping<Book>
    {
        public BookMapping()
        {
            DiscriminatorValue("Book");
            Property(x => x.Author);
            Property(x => x.ISBN);
            ManyToOne(x => x.Author, x =>
            {
                x.Column("PublisherId");
                x.Cascade(Cascade.Persist);
            });
        }
    }
}