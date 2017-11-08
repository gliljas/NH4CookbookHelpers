using System;
using NH4CookbookHelpers.Mapping;
using NHibernate;

namespace MappingRecipes.LazyProperties
{
    public class Recipe : HbmMappingRecipe
    {
        protected override void AddInitialData(ISession session)
        {
            session.Save(new Article
            {
                Title = "Lazy properties",
                Author = "NHibernate",
                Abstract = "Supporting lazy properties is cool",
                FullText = "An enourmously long text"
            });
        }

        public override void RunQueries(ISession session)
        {
            var article = session.Get<Article>(1);
            Console.WriteLine("Title:" + article.Title);
            Console.WriteLine("Author:" + article.Author);
            Console.WriteLine("Abstract:" + article.Abstract);
            Console.WriteLine("Has fulltext been loaded: {0}",
                NHibernateUtil.IsPropertyInitialized(article, "FullText"));
            Console.WriteLine("Full text:" + article.FullText);
        }
    }
}