using System;
using NH4CookbookHelpers.Mapping;
using NHibernate;

namespace MappingRecipes.Components
{
    public class Recipe : HbmMappingRecipe
    {
        protected override void AddInitialData(ISession session)
        {
            session.Save(new Customer
            {
                Name = "Max Weinberg",
                BillingAddress = new Address
                {
                    Lines = "E Street 1",
                    City = "Belmar",
                    State = "New Jersey",
                    ZipCode = "123"
                },
                ShippingAddress = new Address
                {
                    Lines = "Home street",
                    City = "Newark",
                    State = "New Jersey",
                    ZipCode = "123"
                }
            });
        }

        public override void RunQueries(ISession session)
        {
            var customer = session.QueryOver<Customer>()
                .SingleOrDefault();
            Console.WriteLine("Customer {0} has a billing address in {1}",
                customer.Name, customer.BillingAddress.City);
        }
    }
}