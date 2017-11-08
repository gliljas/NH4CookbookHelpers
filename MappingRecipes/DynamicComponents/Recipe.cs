using System;
using System.Linq;
using NH4CookbookHelpers.Mapping;
using NHibernate;
using NHibernate.Linq;

namespace MappingRecipes.DynamicComponents
{
    public class Recipe : HbmMappingRecipe
    {
        protected override void AddInitialData(ISession session)
        {
            session.Save(new Contact
            {
                Attributes =
                {
                    ["FirstName"] = "Dave",
                    ["LastName"] = "Gahan",
                    ["BirthDate"] = new DateTime(1962, 5, 9)
                }
            });
            session.Save(new Contact
            {
                Attributes =
                {
                    ["FirstName"] = "Martin",
                    ["LastName"] = "Gore",
                    ["BirthDate"] = new DateTime(1961, 7, 23)
                }
            });
        }

        public override void RunQueries(ISession session)
        {
            var contactsBornInMay = session.Query<Contact>()
                .Where(x => ((DateTime) x.Attributes["BirthDate"]).Month == 5)
                .ToList();
            foreach (var contact in contactsBornInMay)
                Console.WriteLine("{0} {1} {2:d}",
                    contact.Attributes["FirstName"],
                    contact.Attributes["LastName"],
                    contact.Attributes["BirthDate"]);
        }
    }
}