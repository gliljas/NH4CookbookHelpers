using Eg.Core;
using NH4CookbookHelpers.Mapping;
using NHibernate;
using NHibernate.Cfg;

namespace MappingRecipes.MappingWithXml
{
    public class Recipe : BaseMappingRecipe
    {
        protected override void Configure(Configuration cfg)
        {
            cfg.AddAssembly(typeof(Product).Assembly);
        }

        protected override void AddInitialData(ISession session)
        {
            session.Save(new Product
            {
                Name = "Car",
                Description = "A nice red car",
                UnitPrice = 300
            });
        }
    }
}