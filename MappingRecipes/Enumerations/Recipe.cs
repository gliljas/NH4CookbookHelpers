using System;
using NH4CookbookHelpers.Mapping;
using NHibernate;

namespace MappingRecipes.Enumerations
{
    public class Recipe : HbmMappingRecipe
    {
        protected override void AddInitialData(ISession session)
        {
            session.Save(new Account
            {
                Name = "Test account",
                Number = "1",
                AcctType = AccountTypes.Consumer
            });
        }

        public override void RunQueries(ISession session)
        {
            var accounts = session.QueryOver<Account>()
                .OrderBy(x => x.Name).Asc
                .List();

            foreach (var account in accounts)
                Console.WriteLine("Account name: {0},type: {1}", account.Name, account.AcctType);
        }
    }
}