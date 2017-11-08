using System;
using NH4CookbookHelpers.Model;

namespace SessionRecipes.UsingTransactionScope
{
    public class WarehouseFacade : IReceiveProductUpdates
    {
        public void Add(Product product)
        {
            Console.WriteLine("Adding {0} to warehouse system.",
                product.Name);
        }

        public void Update(Product product)
        {
            Console.WriteLine("Updating {0} in warehouse system.",
                product.Name);
        }

        public void Remove(Product product)
        {
            Console.WriteLine("Removing {0} from warehouse system.",
                product.Name);
            var message = string.Format(
                "Warehouse still has inventory of {0}.",
                product.Name);
            throw new ApplicationException(message);
        }

    }
}