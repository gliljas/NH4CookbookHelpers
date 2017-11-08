using System;
using NH4CookbookHelpers;
using NHibernate;

namespace QueryRecipes.HqlBulkChanges
{
    public class Recipe : QueryRecipe
    {
        protected override void Run(ISession session)
        {
            var hql = @"update Book b 
                        set b.UnitPrice = :minPrice
                        where b.UnitPrice < :minPrice";

            var updated=session.CreateQuery(hql)
              .SetDecimal("minPrice", 55M)
              .ExecuteUpdate();

            Console.WriteLine("Number of books updated:" + updated);

            hql = @"delete from Book
                    where UnitPrice=:minPrice";
            var deleted = session.CreateQuery(hql)
              .SetDecimal("minPrice", 55M)
              .ExecuteUpdate();

            Console.WriteLine("Number of books deleted:" + deleted);

            hql = @"insert into Book (Name,Description) 
                    select concat(Name,' - the book'),Description 
                    from Movie";
            var inserted = session.CreateQuery(hql)
              .ExecuteUpdate();

            Console.WriteLine("Number of movies recreated as books:" + inserted);

        }
    }
}
