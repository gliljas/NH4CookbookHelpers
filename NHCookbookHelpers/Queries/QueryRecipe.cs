using System;
using System.Collections.Generic;
using System.Linq;
using NH4CookbookHelpers.Mapping;
using NH4CookbookHelpers.Model;
using NHibernate;
using NHibernate.Cfg;

namespace NH4CookbookHelpers
{
    public abstract class QueryRecipe : RecipeWithDatabase
    {
        

        protected virtual void Run(ISession session)
        {
        }

        protected virtual void Run(ISessionFactory sessionFactory)
        {
        }

        protected internal override void AddBaseMappings(Configuration cfg)
        {
            ProductModel.AddBaseMappings(cfg);
        }

        internal override void RunRecipe()
        {
            LogEnabled = false;
            ProductModel.AddBaseData(SessionFactory);
            LogEnabled = true;
            AddData(SessionFactory);
            SessionFactory.Statistics.Clear();
            Run(SessionFactory);
            using (var session = SessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    Run(session);
                    tx.Commit();
                }
            }
        }

        protected void ShowQueryResults(IQueries queries)
        {
            Show("Movies directed by Spielberg:",
              queries.GetMoviesDirectedBy(
              "Steven Spielberg"));

            Show("Movies with Morgan Freeman:",
              queries.GetMoviesWith(
              "Morgan Freeman"));

            Show("This book:",
              queries.GetBookByISBN(
              "978-1-849513-04-3"));

            Show("Cheap products:",
              queries.GetProductsByPrice(0M, 15M));
        }

        protected void ShowAggregateQueryResults(IAggregateQueries queries)
        {
            Show("Movie Price List:",
              queries.GetMoviePriceList());

            Show("Average Movie Price:",
              queries.GetAverageMoviePrice());

            Show("Average Price by Director:",
              queries.GetAvgDirectorPrice());
        }

        private void Show(string heading,
                 IEnumerable<NameAndPrice> results)
        {
            Console.WriteLine(heading);
            foreach (var item in results)
                ShowNameAndPrice(item);
            Console.WriteLine();
        }

        private void ShowNameAndPrice(NameAndPrice item)
        {
            Console.WriteLine("{0:c} {1}",
                              item.Price, item.Name);
        }

        private void Show(string heading,
                 decimal moneyValue)
        {
            Console.WriteLine(heading);
            Console.WriteLine("{0:c}", moneyValue);
            Console.WriteLine();
        }



        private void Show(string heading, IEnumerable<Movie> movies)
        {
            Console.WriteLine(heading);
            foreach (var m in movies)
                ShowMovie(m);
            Console.WriteLine();

        }

        private void ShowMovie(Movie movie)
        {
            if (movie != null)
            {
                var stars = string.Join(" & ", movie.Actors.Select(x => x.Actor));
                if (stars == "")
                {
                    stars = null;
                }

                Console.WriteLine("{0:c} {1} starring {2}", movie.UnitPrice, movie.Name, stars ?? "nobody");
            }
            else
            {
                Console.WriteLine("Not found");
            }

        }

        protected void Show(string heading, Book book)
        {
            Console.WriteLine(heading);
            ShowBook(book);
            Console.WriteLine();

        }

        protected void ShowBook(Book book)
        {
            if (book != null)
            {
                Console.WriteLine("{0:c} {1}  - {3} (ISBN {2})", book.UnitPrice, book.Name, book.ISBN, book.Publisher != null ? book.Publisher.Name : "");
            }
            else
            {
                Console.WriteLine("Not found");
            }
        }

        protected void Show(string heading, IEnumerable<Product> products)
        {
            Console.WriteLine(heading);
            foreach (var p in products)
            {
                if (p is Movie)
                {
                    ShowMovie((Movie)p);
                }
                else if (p is Book)
                {
                    ShowBook((Book)p);
                }
                else
                    ShowProduct(p);
            }
            Console.WriteLine();

        }

        protected void ShowProduct(Product product)
        {
            Console.WriteLine("{2}: {0:c} {1}", product.UnitPrice, product.Name, product.GetType().Name);
        }

        

        protected virtual void AddData(ISessionFactory sessionFactory)
        {

        }

        

        protected void ShowNumberOfQueriesExecuted()
        {
            Console.WriteLine("Number of queries executed:" + SessionFactory.Statistics.PrepareStatementCount);
        }


    }
}
