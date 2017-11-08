using FluentNHibernate.Mapping;
using NH4CookbookHelpers.Mapping.Model;

namespace MappingRecipes.MappingWithFluent
{
    public class BookMap : SubclassMap<Book>
    {
        public BookMap()
        {
            DiscriminatorValue("Book");
            Map(p => p.Author);
            Map(p => p.ISBN);
        }
    }
}