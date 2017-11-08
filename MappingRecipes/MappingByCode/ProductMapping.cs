using NH4CookbookHelpers.Mapping.Model;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MappingRecipes.MappingByCode
{
    public class ProductMapping : ClassMapping<Product>
    {
        public ProductMapping()
        {
            Table("Product");
            Id(x => x.Id, x => x.Generator(Generators.GuidComb));
            Version(p => p.Version, v => v.UnsavedValue(0));
            NaturalId(p => p.Property(x => x.Name), p => p.Mutable(true));
            Discriminator(p => p.Column("ProductType"));
            Property(p => p.Name);
            Property(p => p.Description);
            Property(p => p.UnitPrice);
        }
    }
}