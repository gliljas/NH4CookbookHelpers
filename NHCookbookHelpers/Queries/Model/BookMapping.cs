using NHibernate.Mapping.ByCode.Conformist;

namespace NH4CookbookHelpers.Model
{
    public class BookMapping : SubclassMapping<Book>
    {
        public BookMapping()
        {
            DiscriminatorValue("Book");
            Property(x => x.Author);
            Property(x => x.ISBN);
            ManyToOne(x => x.Publisher, x => x.Column("PublisherId"));
        }
    }
}