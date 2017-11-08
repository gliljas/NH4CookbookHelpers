using System;
using NH4CookbookHelpers;
using NH4CookbookHelpers.Model;
using NHibernate;

namespace QueryRecipes.QueryById
{
    public class Recipe : QueryRecipe
    {
        protected override void Run(ISession session)
        {
            var product1 = session.Get<Product>(1);
            ShowNumberOfQueriesExecuted();
            ShowProduct(product1);
           
            var product2 = session.Load<Product>(2);

            ShowNumberOfQueriesExecuted();
            ShowProduct(product2);
            ShowNumberOfQueriesExecuted();

            var movie2 = session.Get<Movie>(1);

            ShowProduct(movie2);
            ShowNumberOfQueriesExecuted();

            var product4 = session.Get<Product>(4);
            Console.WriteLine(session.Load<Product>(4)==null);
            
        }
    }
}
