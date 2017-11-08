using NH4CookbookHelpers;
using NH4CookbookHelpers.Model;
using NHibernate;
using NHibernate.Criterion;

namespace QueryRecipes.DetachedQueries
{
    public class Recipe : QueryRecipe
    {
        protected override void Run(ISession session)
        {
            var isbn = "3043";

            var query = DetachedCriteria.For<Book>()
              .Add(Restrictions.Eq("ISBN", isbn));

            var book = query.GetExecutableCriteria(session)
            .UniqueResult<Book>();

            Show("Book with ISBN=3043",book);
        }
    }
}
