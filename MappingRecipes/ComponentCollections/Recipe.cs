using NH4CookbookHelpers.Mapping;
using NHibernate;

namespace MappingRecipes.ComponentCollections
{
    public class Recipe : HbmMappingRecipe
    {
        protected override void AddInitialData(ISession session)
        {
            session.Save(new Customer
            {
                Name = "Max Weinberg",
                Addresses =
                {
                    new Address {AddressLine1 = "E Street 1", City = "Belmar, NJ"},
                    new Address {AddressLine1 = "Home street", City = "Newark, NJ"}
                },
                Tags = {"Drummer", "Bruce"}
            });
        }
    }
}