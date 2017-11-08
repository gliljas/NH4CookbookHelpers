using System;
using System.Data;

namespace NH4CookbookHelpers
{
    public interface IRecipeLogger
    {
        void ShowTable(string name,DataTable schema, DataTable dataTable);
        void LogMessage(string message);
    }
}