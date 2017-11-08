using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NH4CookbookHelpers.Model
{
    public class ProductMapping : ClassMapping<Product>
    {
        public ProductMapping()
        {
            Discriminator(x => x.Column("ProductType"));
            DiscriminatorValue("Product");
            Id(x => x.Id, x => x.Generator(new NativeGeneratorDef()));
            NaturalId(x => x.Property(p => p.Name), x => { x.Mutable(true);});
            Property(x => x.Description);
            Property(x => x.UnitPrice);
        }
    }
}