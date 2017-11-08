using System;
using System.Threading;
using NH4CookbookHelpers;
using NH4CookbookHelpers.Model;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace SessionRecipes.SessionLock
{
    public class Recipe : QueryRecipe
    {
        protected override void Configure(Configuration nhConfig)
        {
           nhConfig.DataBaseIntegration(db =>
           {
               db.Dialect<MsSql2012Dialect>();
               db.Driver<Sql2008ClientDriver>();
               db.ConnectionString =
                 @"Server=.;Database=NHCookbook;
Trusted_Connection=True;";
           });

        }

        protected override void Run(ISessionFactory sessionFactory)
        {
            ExecuteWithLockMode(sessionFactory, LockMode.Read, LockMode.Read);
            ExecuteWithLockMode(sessionFactory, LockMode.Upgrade, LockMode.Upgrade);
            ExecuteWithLockMode(sessionFactory, LockMode.Upgrade, LockMode.Read);
            ExecuteWithLockMode(sessionFactory, LockMode.UpgradeNoWait, LockMode.UpgradeNoWait);
        }

        private void ExecuteWithLockMode(ISessionFactory sessionFactory, LockMode lockMode1, LockMode lockMode2)
        {
            Console.WriteLine("Executing with {0} and {1}", lockMode1, lockMode2);
            Console.WriteLine();

            var thread1 = new Thread(() => GetAndChangeProductInLock(sessionFactory, lockMode1, 3000)) { Name = "Thread1" };
            var thread2 = new Thread(() => GetAndChangeProductInLock(sessionFactory, lockMode2, 0)) { Name = "Thread2" };

            thread1.Start();
            Thread.Sleep(300);

            thread2.Start();

            thread1.Join();
            thread2.Join();
            Console.WriteLine();
        }

        private void GetAndChangeProductInLock(ISessionFactory sessionFactory, LockMode lockMode, int sleepTime)
        {
            try
            {
                using (var session = sessionFactory.OpenSession())
                {
                    using (var tx = session.BeginTransaction())
                    {
                        Console.WriteLine("{0} acquiring lock", Thread.CurrentThread.Name);
                        var product = session.Get<Product>(1, lockMode);
                        //session.Lock(product, lockMode);
                        Console.WriteLine("{0} acquired lock", Thread.CurrentThread.Name);
                        product.Description = string.Format("Updated in LockMode.{0}",lockMode);
                        Thread.Sleep(sleepTime);
                       
                        tx.Commit();
                        Console.WriteLine("{0} committed", Thread.CurrentThread.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in {0}:{1}", Thread.CurrentThread.Name, ex.Message);
            }
        }
    }
}
