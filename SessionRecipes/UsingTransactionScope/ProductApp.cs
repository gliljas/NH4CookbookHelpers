using System;
using System.Transactions;
using NH4CookbookHelpers.Model;

namespace SessionRecipes.UsingTransactionScope
{
    public class ProductApp
    {
        private readonly IReceiveProductUpdates[] _services;

        public ProductApp(params IReceiveProductUpdates[] services)
        {
            _services = services;
        }

        public void AddProduct(Product newProduct)
        {
            Console.WriteLine("Adding {0}.", newProduct.Name);
            try
            {
                using (var scope = new TransactionScope())
                {
                    foreach (var service in _services)
                        service.Add(newProduct);
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Product could not be added.");
                Console.WriteLine(ex.Message);
            }

        }

        public void UpdateProduct(Product changedProduct)
        {
            Console.WriteLine("Updating {0}.",
                changedProduct.Name);
            try
            {
                using (var scope = new TransactionScope())
                {
                    foreach (var service in _services)
                        service.Update(changedProduct);
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Product could not be updated.");
                Console.WriteLine(ex.Message);
            }
        }

        public void RemoveProduct(Product oldProduct)
        {
            Console.WriteLine("Removing {0}.",
                oldProduct.Name);
            try
            {
                using (var scope = new TransactionScope())
                {
                    foreach (var service in _services)
                        service.Remove(oldProduct);
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Product could not be removed.");
                Console.WriteLine(ex.Message);
            }
        }
    }
}