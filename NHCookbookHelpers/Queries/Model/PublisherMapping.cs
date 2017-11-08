using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NH4CookbookHelpers.Model
{
    public class PublisherMapping : ClassMapping<Publisher>
    {
        public PublisherMapping()
        {
            Id(x => x.Id, x => x.Generator(new GuidCombGeneratorDef()));
            Property(x=>x.Name);
        }
    }
}