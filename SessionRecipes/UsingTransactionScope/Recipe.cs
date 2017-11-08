using NH4CookbookHelpers;
using NH4CookbookHelpers.Model;
using NHibernate;

namespace SessionRecipes.UsingTransactionScope
{
    public class Recipe : QueryRecipe
    {
        protected override void Run(ISessionFactory sessionFactory)
        {
            var catalog = new ProductCatalog(sessionFactory);
            var warehouse = new WarehouseFacade();

            var p = new ProductApp(catalog, warehouse);

            var sprockets = new Product()
            {
                Name = "Sprockets",
                Description = "12 pack, metal",
                UnitPrice = 14.99M
            };

            p.AddProduct(sprockets);

            sprockets.UnitPrice = 9.99M;
            p.UpdateProduct(sprockets);

            p.RemoveProduct(sprockets);
        }
    }
}
