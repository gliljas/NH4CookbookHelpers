using System;
using NH4CookbookHelpers.Mapping;
using NHibernate;

namespace MappingRecipes.Versioning
{
    public class Recipe : HbmMappingRecipe
    {
        protected override void AddInitialData(ISession session)
        {
            session.Save(new VersionedProduct
            {
                Name = "Stuff",
                Description = "Cool"
            });
        }

        public override void RunQueries(ISessionFactory sessionFactory)
        {
            try
            {
                using (var s1 = sessionFactory.OpenSession())
                using (var s2 = sessionFactory.OpenSession())
                using (var tx1 = s1.BeginTransaction())
                using (var tx2 = s2.BeginTransaction())
                {
                    var product1 = s1.Get<VersionedProduct>(1);

                    var product2 = s2.Get<VersionedProduct>(1);

                    product1.Name = "Modified in session 1";

                    product2.Name = "Modified in session 2";

                    tx1.Commit();
                    Console.WriteLine("Commit 1");
                    //This should fail
                    tx2.Commit();
                    Console.WriteLine("Commit 2");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
        }
    }
}