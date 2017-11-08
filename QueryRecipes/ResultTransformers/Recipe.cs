using System;
using NH4CookbookHelpers;
using NH4CookbookHelpers.Model;
using NHibernate;
using NHibernate.Transform;

namespace QueryRecipes.ResultTransformers
{
    public class Recipe : QueryRecipe
    {
        protected override void Run(ISession session)
        {
            var movieQuery = session.QueryOver<Movie>()
                 .Inner.JoinQueryOver(x => x.Actors);

            Console.WriteLine("Result count without transformer:{0}", movieQuery.List<Movie>().Count);

            movieQuery = movieQuery.TransformUsing(Transformers.DistinctRootEntity);

            Console.WriteLine("Result count with transformer:{0}", movieQuery.List<Movie>().Count);

            var bookResults = session.CreateSQLQuery(@"
                select b.Name, b.Author,p.Name as PublisherName from Product b
                left join Publisher p ON b.PublisherId=p.Id
                where b.ProductType = 'Book'")
                 .SetResultTransformer(Transformers.AliasToBean<BookInfo>())
                 .List<BookInfo>();


            Console.WriteLine("BookInfo objects:");
            foreach (var result in bookResults)
            {
                Console.WriteLine("{0}, by {1}, published by {2}",
                     result.Name,
                     result.Author,
                     result.PublisherName);
            }
        }
    }
}
