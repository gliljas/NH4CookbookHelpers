using System;
using NH4CookbookHelpers.Model;
using NHibernate;

namespace SessionRecipes.UsingTransactionScope
{
    public class ProductCatalog : IReceiveProductUpdates
    {

        private readonly ISessionFactory _sessionFactory;

        public ProductCatalog(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public void Add(Product product)
        {
            Console.WriteLine("Adding {0} to product catalog.",
                product.Name);
            using (var session = _sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                session.Save(product);
                tx.Commit();
            }
        }

        public void Update(Product product)
        {
            Console.WriteLine("Updating {0} in product catalog.",
                product.Name);
            using (var session = _sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                session.Update(product);
                tx.Commit();
            }
        }

        public void Remove(Product product)
        {
            Console.WriteLine("Removing {0} from product cata-log.",
                product.Name);
            using (var session = _sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                session.Delete(product);
                tx.Commit();
            }
        }

    }
}