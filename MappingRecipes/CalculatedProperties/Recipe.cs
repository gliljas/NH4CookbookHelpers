using System;
using NH4CookbookHelpers.Mapping;
using NHibernate;

namespace MappingRecipes.CalculatedProperties
{
    public class Recipe : HbmMappingRecipe
    {
        protected override void AddInitialData(ISession session)
        {
            session.Save(new Invoice {Amount = 200, Customer = "A"});
            session.Save(new Invoice {Amount = 2000, Customer = "A"});
            session.Save(new Invoice {Amount = 200, Customer = "B"});
        }

        public override void RunQueries(ISession session)
        {
            var invoices = session.QueryOver<Invoice>().List();
            foreach (var invoice in invoices)
                Console.WriteLine("Amount: {0}, InvoicesOnCustomer: {1}",
                    invoice.Amount, invoice.InvoicesOnCustomer);
        }
    }
}