using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH4CookbookHelpers
{
    internal interface IRecipe : IDisposable
    {
        void Initialize(IRecipeLogger logger);
        void Run();
        bool LogEnabled { get; set; }
    }
}
