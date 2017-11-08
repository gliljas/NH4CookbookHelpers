using System;
using NH4CookbookHelpers;
using NH4CookbookHelpers.Model;
using NHibernate;

namespace SessionRecipes.SessionRefresh
{
    public class Recipe : QueryRecipe
    {
        protected override void Run(ISessionFactory sessionFactory)
        {
            using (var sessionA = sessionFactory.OpenSession())
            using (var sessionB = sessionFactory.OpenSession())
            {

                int productId;

                var productA = new Product()
                {
                    Name = "Lawn Chair",
                    Description = "Lime Green, Comfortable",
                    UnitPrice = 10.00M
                };

                using (var tx = sessionA.BeginTransaction())
                {
                    Console.WriteLine("Saving product in session A.");
                    productId = (int)sessionA.Save(productA);
                    tx.Commit();
                }

                using (var tx = sessionB.BeginTransaction())
                {
                    Console.WriteLine("Changing price in session B.");
                    var productB = sessionB.Get<Product>(productId);
                    productB.UnitPrice = 15.00M;
                    tx.Commit();
                }

                using (var tx = sessionA.BeginTransaction())
                {
                    Console.WriteLine("Price was {0:c}",
                        productA.UnitPrice);

                    Console.WriteLine("Refreshing");

                    sessionA.Refresh(productA);

                    Console.WriteLine("Price is {0:c}",
                        productA.UnitPrice);
                    tx.Commit();
                }
            }
        }
    }
}
