using System;
using NH4CookbookHelpers;
using NH4CookbookHelpers.Model;
using NHibernate;

namespace SessionRecipes.SessionMerge
{
    public class Recipe : QueryRecipe
    {
        protected override void Run(ISessionFactory sessionFactory)
        {
            var book = CreateAndSaveBook(sessionFactory);
            book.Name = "Dormice in Action";
            book.Description = "Hibernation of the Hazel Dormouse";
            book.UnitPrice = 0.83M;
            book.ISBN = "0123";

            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    var mergedBook = (Book)session.Merge(book);
                    tx.Commit();

                    Console.WriteLine(ReferenceEquals(book, mergedBook));
                }
            }
        }

        private static Book CreateAndSaveBook(
  ISessionFactory sessionFactory)
        {
            var book = new Book()
            {
                Name = "The book of awesomeness",
                Description = "Pure Awesome",
                UnitPrice = 50.0M,
                ISBN = "3043",
                Author = "Awe Some",
            };

            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    session.Save(book);
                    tx.Commit();
                    session.Evict(book);
                }
            }
            return book;
        }
    }
}
