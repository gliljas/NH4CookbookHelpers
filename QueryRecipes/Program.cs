using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NH4CookbookHelpers;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace QueryRecipes
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            RecipeLoader.DefaultConfiguration = () => new Configuration().DataBaseIntegration(db =>
              {
                  db.Dialect<MsSql2012Dialect>();
                  db.Driver<Sql2008ClientDriver>();
                  db.ConnectionString = "Server=.;Database=NHCookbook;Trusted_Connection=True;";
              });
            Application.Run(new WindowsFormsRunner());
        }
    }
}
