using System;
using NH4CookbookHelpers.Mapping;
using NHibernate;

namespace MappingRecipes.PropertyRefs
{
    public class Recipe : HbmMappingRecipe
    {
        protected override void AddInitialData(ISession session)
        {
            var customer = new Customer
            {
                Name = "The customer",
                CompanyId = 345
            };

            customer.ContactPersons.Add(
                new ContactPerson
                {
                    Customer = customer,
                    Name = "Person1"
                }
            );

            session.Save(customer);
        }

        public override void RunQueries(ISession session)
        {
            var customer = session.Get<ContactPerson>(1);
            Console.WriteLine("Customer:" + customer.Customer.Name);
        }
    }
}