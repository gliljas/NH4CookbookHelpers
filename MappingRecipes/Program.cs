using System;
using System.Windows.Forms;
using NH4CookbookHelpers;

namespace MappingRecipes
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //RecipeLoader.DefaultConfiguration = new Configuration().DataBaseIntegration(db =>
            //{
            //    db.Dialect<PostgreSQL82Dialect>();
            //    db.Driver<NpgsqlDriver>();
            //    db.ConnectionString = "Server=localhost;Database=NHCookbook;User Id=cookbook;Password = cookbook; ";
            //});
            Application.Run(new WindowsFormsRunner());
        }
    }
}