using System.Data;

namespace NH4CookbookHelpers
{
    public class NullLogger : IRecipeLogger
    {
        public void LogMessage(string message)
        {
        }

        public void ShowTable(string name, DataTable schema, DataTable dataTable)
        {
        }
    }
}