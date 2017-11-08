using FluentNHibernate.Mapping;
using NH4CookbookHelpers.Mapping.Model;

namespace MappingRecipes.MappingWithFluent
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Id(p => p.Id).GeneratedBy.GuidComb();
            Version(x => x.Version);
            NaturalId().Property(p => p.Name).Not.ReadOnly();
            DiscriminateSubClassesOnColumn("ProductType");
            Map(p => p.Description);
            Map(p => p.UnitPrice);
        }
    }
}